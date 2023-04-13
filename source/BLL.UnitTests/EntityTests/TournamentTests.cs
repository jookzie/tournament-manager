using Modules.Entities;
using Modules.Entities.ScheduleTypes;
using Modules.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BLL.UnitTests.EntityTests
{
    public class TournamentTests
    {
        [Test]
        public void TestTournament_Valid()
        {
            new Tournament
            {
                TournamentSystem = typeof(RoundRobin),
                SportType = SportType.Badminton,
                Location = "Location",
                Description = "Description",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (9, 10),
                Players = new List<User>() { new User() }
            };
        }
        // For some reason, using the [Range] attribute makes the whole test class not run
        //[Test]
        //public void TestTournament_Valid(
        //    [Range(1, 200)] string location, 
        //    [Range(1, 200)] string description,
        //    [Range(3, 100)] int minCapacity,
        //    [Range(3, 100)] int maxCapacity)
        //{
        //    new Tournament
        //    {
        //        TournamentSystem = typeof(RoundRobin),
        //        SportType = SportType.Badminton,
        //        Location = location,
        //        Description = description,
        //        DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
        //        CapacityRange = (minCapacity, maxCapacity),
        //        Players = new List<User>() { new User() }
        //    };
        //}
        [Test]
        public void TestTournament_EmptyDescription()
        {
            void act() => new Tournament
            {
                Description = "",
            };
            Assert.Throws<ArgumentNullException>(act);
        }
        [Test]
        public void TestTournament_EmptyLocation()
        {
            void act() => new Tournament
            {
                Location = "",
            };
            Assert.Throws<ArgumentNullException>(act);
        }
        [TestCase(201)]
        public void TestTournament_Location_UpperBound(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Tournament
            {
                Location = string.Concat(Enumerable.Repeat("0", length))
            });
        }
        [TestCase(201)]
        public void TestTournament_Description_UpperBound(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Tournament
            {
                Description = string.Concat(Enumerable.Repeat("0", length))
            });
        }
        [TestCase("2022/01/02", "2022/01/01")]
        public void TestTournament_DateRange_InvalidOperations(string start, string end)
        {
            Assert.Throws<InvalidOperationException>(() =>
            new Tournament
            {
                DateRange = (DateTime.Parse(start), DateTime.Parse(end))
            });
        }
        [TestCase(5, 3)]
        public void TestTournament_CapacityRange_InvalidOperations(int min, int max)
        {
            Assert.Throws<InvalidOperationException>(() =>
            new Tournament
            {
                CapacityRange = (min, max)
            });
        }

        [TestCase(99, 101)]
        [TestCase(101, 102)]
        [TestCase(2, 2)]
        [TestCase(2, 10)]
        public void TestTournament_CapacityRange_Bounds(int min, int max)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Tournament
            {
                CapacityRange = (min, max)
            });
        }
        [Test]
        public void TestTournament_PlayerCount_BiggerThan_MaxCapacity()
        {
            void act() => new Tournament
            {
                CapacityRange = (3, 3),
                Players = new List<User> { new User(), new User(), new User(), new User() }
            };
            Assert.Throws<InvalidOperationException>(act);
        }
        [TestCase(typeof(RoundRobin))]
        public void TestTournament_GenerateSchedule_Valid(Type system)
        {
            // Arrange
            var tournament = new Tournament
            {
                TournamentSystem = system,
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (3, 3),
                Players = new List<User>() { new User(), new User(), new User() }
            };
            // Act
            tournament.GenerateSchedule();
            // Assert
            Assert.IsNotNull(tournament.Schedule);
        }

        [TestCase(typeof(RoundRobin))]
        [TestCase(typeof(DoubleRoundRobin))]
        public void TestTournament_GenerateSchedule_CorrectType(Type tournamentSystem)
        {
            // Arrange
            var tournament = new Tournament
            {
                TournamentSystem = tournamentSystem,
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (3, 3),
                Players = new List<User>() { new User(), new User(), new User() }
            };
            // Act
            tournament.GenerateSchedule();
            // Assert
            if (tournament.TournamentSystem == tournament.Schedule.GetType())
                Assert.Pass();
            else
                Assert.Fail($"GenerateSchedule() method generated a different schedule type - {tournament.Schedule.GetType()} instead of {tournament.TournamentSystem}.");
        }
        [Test]
        public void TestTournament_GetTournamentTypes()
        {
            foreach(var system in Tournament.GetTournamentTypes())
            {
                var tournament = new Tournament
                {
                    TournamentSystem = system,
                    SportType = SportType.Badminton,
                    Location = "Unit testing",
                    Description = "Unit testing",
                    DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                    CapacityRange = (3, 3),
                    Players = new List<User>() { new User(), new User(), new User() }
                };
                tournament.GenerateSchedule();
                Assert.IsNotNull(tournament.Schedule);
            }
        }
        [Test]
        public void TestTournament_ClearSchedule()
        {
            // Arrange
            var tournament = new Tournament
            {
                TournamentSystem = typeof(RoundRobin),
                SportType = SportType.Badminton,
                Location = "Unit testing",
                Description = "Unit testing",
                DateRange = (DateTime.Now, DateTime.Now.AddDays(1)),
                CapacityRange = (3, 3),
                Players = new List<User>() { new User(), new User(), new User() }
            };
            tournament.GenerateSchedule();
            // Act
            tournament.ClearSchedule();
            // Assert
            Assert.IsNull(tournament.Schedule);
        }
    }
}