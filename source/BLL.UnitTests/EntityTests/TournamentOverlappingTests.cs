using Modules.Entities;
using NUnit.Framework;
using System;

namespace BLL.UnitTests.EntityTests
{
    public class TournamentOverlappingTests
    {
        [Test]
        public void TestOverlapping_1001()
        {
            var tournament = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 10),
                new DateTime(2022, 6, 20))
            };
            var tournament2 = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 9),
                new DateTime(2022, 6, 21))
            };
            Assert.IsTrue(tournament.Overlaps(tournament2));
        }
        [Test]
        public void TestOverlapping_1010()
        {
            var tournament = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 10), new DateTime(2022, 6, 20))
            };
            var tournament2 = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 9), new DateTime(2022, 6, 19))
            };
            Assert.IsTrue(tournament.Overlaps(tournament2));
        }
        [Test]
        public void TestOverlapping_0101()
        {
            var tournament = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 10),
                new DateTime(2022, 6, 20))
            };
            var tournament2 = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 11),
                new DateTime(2022, 6, 21))
            };
            Assert.IsTrue(tournament.Overlaps(tournament2));
        }
        [Test]
        public void TestOverlapping_0110()
        {
            var tournament = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 10),
                new DateTime(2022, 6, 20))
            };
            var tournament2 = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 11),
                new DateTime(2022, 6, 19))
            };
            Assert.IsTrue(tournament.Overlaps(tournament2));
        }
        [Test]
        public void TestOverlapping_SameTournament()
        {
            var tournament = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 10),
                new DateTime(2022, 6, 20))
            };
            Assert.IsTrue(tournament.Overlaps(tournament));
        }
        [Test]
        public void TestOverlapping_EndDate_StartDate()
        {
            var tournament = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 10),
                new DateTime(2022, 6, 20))
            };
            var tournament2 = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 20),
                new DateTime(2022, 6, 30))
            };

            Assert.IsTrue(tournament.Overlaps(tournament2));
        }
        [Test]
        public void TestOverlapping_StartDate_EndDate()
        {
            var tournament = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 10),
                new DateTime(2022, 6, 20))
            };
            var tournament2 = new Tournament
            {
                DateRange = (new DateTime(2022, 6, 5),
                new DateTime(2022, 6, 10))
            };

            Assert.IsTrue(tournament.Overlaps(tournament2));
        }

    }
}