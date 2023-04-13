using BLL.UnitTests.Mocks.BLL;
using BLL.UnitTests.Mocks.DAL;
using Modules.Entities;
using Modules.Enums;
using Modules.Interfaces.BLL;
using Modules.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BLL.UnitTests.ManagerTests
{
    internal class TournamentManagerTests
    {
        private TournamentManager _manager;
        private IUserManager _userManager;
        [SetUp]
        public void Setup()
        {
            _userManager = new MockUserManager();
            _manager = new TournamentManager(
                _userManager,
                new MockTournamentRepository());
        }
        [Test]
        public void Test_AddTournament()
        {
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (9, 10),
                Players = new List<User>() { new User() }
            };
            // Act
            _manager.AddTournament(tournament);
            // Assert
            var readTournament = _manager.GetTournamentBy(tournament.ID);
            Assert.AreEqual(readTournament, tournament);
        }
        [Test]
        public void Test_ReadTournament()
        {
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (9, 10),
                Players = new List<User>() { new User() }
            };
            _manager.AddTournament(tournament);
            // Act
            var readTournament = _manager.GetTournamentBy(tournament.ID);
            // Assert
            Assert.AreEqual(readTournament, tournament);
        }
        [Test]
        public void Test_UpdateTournament()
        {
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (9, 10),
                Players = new List<User>() { new User() }
            };
            _manager.AddTournament(tournament);
            // Act
            var updatedTournament = tournament with
            {
                Location = "Update",
                Description = "Update",
                DateRange = (DateTime.Now.AddDays(1), DateTime.Now.AddDays(5)),
                CapacityRange = (11, 12),
            };
            _manager.UpdateTournament(updatedTournament);
            // Assert
            var readTournament = _manager.GetTournamentBy(tournament.ID);
            Assert.AreEqual(readTournament, updatedTournament);
        }
        [Test]
        public void Test_DeleteTournament()
        {
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (9, 10),
                Players = new List<User>() { new User() }
            };
            _manager.AddTournament(tournament);
            // Act
            bool isDeleted = _manager.DeleteTournament(tournament);
            // Assert
            var readTournament = _manager.GetTournamentBy(tournament.ID);
            Assert.IsTrue(readTournament == null);
            Assert.IsTrue(isDeleted);
        }
        [Test]
        public void Test_RegisterPlayer()
        {
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now.AddDays(7), DateTime.Now.AddDays(8)),
                CapacityRange = (9, 10)
            };
            _manager.AddTournament(tournament);
            var player = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            _userManager.AddUser(player.ID, player.Name, player.Email, player.HashedPassword, player.AccountType);
            // Act
            bool result = _manager.RegisterPlayer(tournament.ID, player.ID);
            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(tournament.Players.Contains(player));
        }
        [Test]
        public void Test_RegisterPlayer_Overlap()
        {
            // Arrange
            var tournament = new Tournament
            {
                DateRange = (DateTime.Now.AddDays(8), DateTime.Now.AddDays(10)),
                CapacityRange = (9, 10)
            };
            var tournament2 = new Tournament
            {
                DateRange = (DateTime.Now.AddDays(7), DateTime.Now.AddDays(8)),
                CapacityRange = (9, 10)
            };
            _manager.AddTournament(tournament);
            _manager.AddTournament(tournament2);
            var player = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            _userManager.AddUser(player.ID, player.Name, player.Email, player.HashedPassword, player.AccountType);
            // Act
            _manager.RegisterPlayer(tournament.ID, player.ID);
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
                _manager.RegisterPlayer(tournament2.ID, player.ID)
            );
        }
        [Test]
        public void Test_RegisterPlayer_Twice()
        {
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now.AddDays(7), DateTime.Now.AddDays(8)),
                CapacityRange = (9, 10)
            };
            _manager.AddTournament(tournament);
            var player = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            _userManager.AddUser(player.ID, player.Name, player.Email, player.HashedPassword, player.AccountType);
            // Act
            bool result = _manager.RegisterPlayer(tournament.ID, player.ID);
            bool result2 = _manager.RegisterPlayer(tournament.ID, player.ID);
            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(result2);
            Assert.IsTrue(tournament.Players.Count != 2);
        }
        [Test]
        public void Test_WithdrawPlayer()
        {
            var player = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now.AddDays(7), DateTime.Now.AddDays(8)),
                CapacityRange = (9, 10),
                Players = new List<User>() { player }
            };
            _manager.AddTournament(tournament);
            
            _userManager.AddUser(player.ID, player.Name, player.Email, player.HashedPassword, player.AccountType);
            // Act
            bool result = _manager.WithdrawPlayer(tournament.ID, player.ID);
            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(tournament.Players.Contains(player));
        }
        [Test]
        public void Test_WithdrawPlayer_Twice()
        {
            var player = new User
            {
                Name = "John Doe",
                Email = "john.doe@my.home",
                HashedPassword = "password123"
            };
            // Arrange
            var tournament = new Tournament
            {
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now.AddDays(7), DateTime.Now.AddDays(8)),
                CapacityRange = (9, 10),
                Players = new List<User>() { player }
            };
            _manager.AddTournament(tournament);

            _userManager.AddUser(player.ID, player.Name, player.Email, player.HashedPassword, player.AccountType);
            // Act
            bool result = _manager.WithdrawPlayer(tournament.ID, player.ID);
            bool result2 = _manager.WithdrawPlayer(tournament.ID, player.ID);
            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(result2);
            Assert.IsFalse(tournament.Players.Contains(player));
            Assert.IsTrue(tournament.Players.Count == 0);
        }
    }
}
