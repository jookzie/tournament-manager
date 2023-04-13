using Modules.Interfaces.Utilities;
using Modules.Utilities;
using NUnit.Framework;
using System;

namespace BLL.UnitTests.UtilityTests
{
    internal class PasswordHasherTests
    {
        private IPasswordHasher _passwordHasher;

        [SetUp]
        public void Setup()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Test]
        public void TestHashing()
        {
            // Arrange
            string password = "password123";
            string expectedHash = "433SykB+X/NSWHOx263PkKUunnqS466KouDCMDsNMMg=";
            byte[] salt = Convert.FromBase64String("a9tQNRT01W3DuU8k9FGn5w==");
            // Act
            var hash = _passwordHasher.Hash(password, salt);
            // Assert
            Assert.AreEqual(expectedHash, hash);
        }
        [Test]
        public void TestVerify_Valid()
        {
            // Arrange
            string password = "password123";
            string hashAndSalt = "433SykB+X/NSWHOx263PkKUunnqS466KouDCMDsNMMg=:a9tQNRT01W3DuU8k9FGn5w==";
            // Act
            var result = _passwordHasher.Verify(password, hashAndSalt);
            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void TestVerify_Invalid()
        {
            // Arrange
            string password = "password1234";
            string hashAndSalt = "433SykB+X/NSWHOx263PkKUunnqS466KouDCMDsNMMg=:a9tQNRT01W3DuU8k9FGn5w==";
            // Act
            var result = _passwordHasher.Verify(password, hashAndSalt);
            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void TestVerify_FormatException_BadHashFormat()
        {
            // Arrange
            string password = "password123";
            string hashAndSalt = "123:123:123";
            // Act
            void act() => _passwordHasher.Verify(password, hashAndSalt);
            // Assert
            var ex = Assert.Throws<FormatException>(act);
            Assert.AreEqual("The hash and salt must be in the format: hash:salt.", ex.Message);
        }
        [Test]
        public void TestVerify_FormatException_Length()
        {
            // Arrange
            string password = "password123";
            string hashAndSalt = "123:123";
            // Act
            void act() => _passwordHasher.Verify(password, hashAndSalt);
            // Assert
            var ex = Assert.Throws<FormatException>(act);
            Assert.AreEqual($"Expected hashed password length to be 69, it is {hashAndSalt.Length}.", ex.Message);
        }

    }
}
