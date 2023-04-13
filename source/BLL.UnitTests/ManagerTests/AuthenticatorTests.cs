using BLL.UnitTests.Mocks.BLL;
using BLL.UnitTests.Mocks.DAL;
using Modules.Entities;
using Modules.Interfaces.DAL;
using NUnit.Framework;

namespace BLL.UnitTests.ManagerTests
{
    public class AuthenticatorTests
    {
        private Authenticator _authenticator;
        private IUserRepository _userRepository;
        [SetUp]
        public void Setup()
        {
            _userRepository = new MockUserRepository();
            _authenticator = new Authenticator(
                _userRepository,
                new MockPasswordHasher());
        }

        [Test]
        public void TestAuthenticate_Valid()
        {
            // Arrange
            _userRepository.AddUser(new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            });
            // Act
            bool result = _authenticator.Authenticate("john.doe@my.home", "password123");
            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void TestAuthenticate_Invalid_Password()
        {
            // Arrange
            // Act
            bool result = _authenticator.Authenticate("john.doe@my.home", "invalid_password123");
            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void TestAuthenticate_Invalid_Email()
        {
            // Arrange
            // Act
            bool result = _authenticator.Authenticate("john.doe@your.home", "password123");
            // Assert
            Assert.IsFalse(result);
        }
    }
}