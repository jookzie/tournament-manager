using BLL.UnitTests.Mocks.BLL;
using BLL.UnitTests.Mocks.DAL;
using Modules.Entities;
using Modules.Enums;
using Modules.Interfaces.BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL.UnitTests.ManagerTests
{
    internal class UserManagerTests
    {
        private IUserManager _userManager;
        [SetUp]
        public void Setup()
        {
            _userManager = new UserManager(
                new MockPasswordHasher(),
                new MockUserRepository());
        }
        [Test]
        public void Test_AddUser()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe", 
                Email = "john.doe@my.home", 
                HashedPassword = "password123"
            };
            // Act
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            // Assert
            Assert.AreEqual(_userManager.GetUserBy(user.Email), user);
        }
        [Test]
        public void Test_AddUser_DuplicateEmail()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            // Act
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            // Assert
            Assert.Throws<DuplicateNameException>(() =>
                _userManager.AddUser(Guid.NewGuid(), "Jane Doe", user.Email, "password1234", user.AccountType)
            );
            
        }
        [Test]
        public void Test_ReadUserByEmail()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            // Act
            var readUser = _userManager.GetUserBy(user.Email);
            // Assert
            Assert.AreEqual(readUser, user);
        }
        [Test]
        public void Test_ReadUserByID()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123",
                AccountType = AccountType.User
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            // Act
            var readUser = _userManager.GetUserBy(user.ID);
            // Assert
            Assert.AreEqual(readUser, user);
        }
        [Test]
        public void Test_UpdateUser()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            
            var updatedUser = new User
            {
                ID = user.ID,
                Name = "Jane Doe",
                Email = "jane.doe@my.home",
                HashedPassword = "password1234"
            };
            // Act
            _userManager.UpdateUser(
                updatedUser.ID, updatedUser.Name, updatedUser.Email, updatedUser.HashedPassword, updatedUser.AccountType);
            // Assert
            var readUser = _userManager.GetUserBy(user.ID);
            Assert.AreEqual(readUser, updatedUser);
        }
        [Test]
        public void Test_UpdateUser_UpdateLastAdministrator()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123",
                AccountType = AccountType.Admin
            };
            var updatedUser = new User
            {
                ID = user.ID,
                Name = "Jane Doe",
                Email = "jane.doe@my.home",
                HashedPassword = "password1234",
                AccountType = AccountType.User
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            
            Assert.Throws<InvalidOperationException>(() =>
                _userManager.UpdateUser(
                    updatedUser.ID, updatedUser.Name, updatedUser.Email, updatedUser.HashedPassword, updatedUser.AccountType)
            );
        }
        [Test]
        public void Test_UpdateUser_DuplicateEmail()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123",
                AccountType = AccountType.User
            };
            var user2 = new User
            {
                Name = "Jane Doe",
                Email = "jane.doe@my.home",
                HashedPassword = "password1234",
                AccountType = AccountType.User
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            _userManager.AddUser(user2.ID, user2.Name, user2.Email, user2.HashedPassword, user2.AccountType);

            Assert.Throws<DuplicateNameException>(() =>
                _userManager.UpdateUser(
                    user2.ID, user2.Name, user.Email, user2.HashedPassword, user2.AccountType)
            );
        }
        [Test]
        public void Test_UpdateUser_NoSuchUser()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123",
                AccountType = AccountType.User
            };
            var unrelatedUser = new User
            {
                Name = "Jane Doe",
                Email = "jane.doe@my.home",
                HashedPassword = "password1234",
                AccountType = AccountType.User
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);

            Assert.Throws<ArgumentException>(() =>
                _userManager.UpdateUser(
                    unrelatedUser.ID, unrelatedUser.Name, unrelatedUser.Email, unrelatedUser.HashedPassword, unrelatedUser.AccountType)
            );
        }        
        [Test]
        public void Test_DeleteUser()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            // Act
            _userManager.DeleteUser(user);
            // Assert
            var readUser = _userManager.GetUserBy(user.ID);
            Assert.IsTrue(readUser == null);
        }
        [Test]
        public void Test_DeleteLastAdministrator()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123",
                AccountType = AccountType.Admin
            };
            _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, user.AccountType);
            Assert.Throws<InvalidOperationException>(() =>
                _userManager.DeleteUser(user)
            );
        }
        [Test]
        public void Test_GetAllUsers()
        {
            // Arrange
            var list = new List<User>()
            {
                new User
                {
                    Name = "John Doe",
                    Email = "john.doe@my.home",
                    HashedPassword = "password123"
                },
                new User
                {
                    Name = "Jane Doe",
                    Email = "jane.doe@my.home",
                    HashedPassword = "password1234"
                }
            };
            foreach (var user in list)
                _userManager.AddUser(user.ID, user.Name, user.Email, user.HashedPassword, AccountType.User);
            // Act
            var readList = _userManager.GetAllUsers();
            // Assert
            CollectionAssert.AreEquivalent(list, readList);
        }
    }
}
