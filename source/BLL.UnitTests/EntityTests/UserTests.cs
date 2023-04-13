using Modules.Entities;
using NUnit.Framework;
using System;
namespace BLL.UnitTests.EntityTests
{
    public class UserTests
    {
        private const string _name = "John Doe";
        private const string _email = "john.doe@my.home";
        private const string _password = "password123";
        [Test]
        public void Test_Valid()
        {
            new User
            {
                Name = _name,
                Email = _email,
                HashedPassword = _password
            };
        }
        [Test]
        [TestCase(_name, _email, "")]
        [TestCase(_name, "", _password)]
        public void Test_EmptyField(string name, string email, string password)
        {
            Assert.Throws<ArgumentNullException>(() =>
            new User
            {
                Name = name,
                Email = email,
                HashedPassword = password
            });
        }
        [Test]
        [TestCase(_name, "john.doeATmy.home", _password)]
        public void Test_InvalidFormat(string name, string email, string password)
        {
            Assert.Throws<FormatException>(() =>
            new User
            {
                Name = name,
                Email = email,
                HashedPassword = password
            });
        }

        // Could not find how to unit test upper bounds of name from NUnit
        private const string _65lengthString = "plawmprbbxzeyitxjmjihdcxkjuyvtnsktmdbnnmprmowffyvhtqnkvnkozuwdxhh";
        private const string _65lengthEmail = "plawmprbbxzeyitxjmjihdcxkjuyvtnsktmdbnnmprmowffyvhtqnkvnk@my.home";
        [Test]
        [TestCase("J", _email, _password)]
        [TestCase(_65lengthString, _email, _password)]
        [TestCase(_name, _65lengthEmail, _password)]
        public void Test_OutOfRange_Name(string name, string email, string password)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            new User
            {
                Name = name,
                Email = email,
                HashedPassword = password
            });
        }
    }
}