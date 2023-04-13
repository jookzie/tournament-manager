using Modules.Entities;
using Modules.Entities.ScheduleTypes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BLL.UnitTests.EntityTests
{
    internal class RoundRobinTests
    {
        private RoundRobin scheduleOdd;
        private RoundRobin scheduleEven;
        private List<User> playersEven = new ()
        {
            new User(),
            new User(),
            new User(),
            new User(),
            new User(),
            new User()
        };
        private List<User> playersOdd = new()
        {
            new User(),
            new User(),
            new User(),
            new User(),
            new User(),
            new User(),
            new User()
        };
        public RoundRobinTests()
        {
            scheduleOdd = new RoundRobin(playersOdd);
            scheduleEven = new RoundRobin(playersEven);
        }
        [Test]
        public void Test_RoundCount_Even()
        {
            Assert.AreEqual(scheduleEven.Rounds.Count, playersEven.Count - 1);
        }
        [Test]
        public void Test_RoundCount_Odd()
        {
            Assert.AreEqual(scheduleOdd.Rounds.Count, playersOdd.Count);
        }
        [Test]
        public void Test_MatchCount_Even()
        {
            foreach (var match in scheduleEven.Rounds.Select(r => r.Matches))
            {
                Assert.AreEqual(match.Count, playersEven.Count / 2);
            }
        }
        [Test]
        public void Test_MatchCount_Odd()
        {
            foreach(var match in scheduleOdd.Rounds.Select(r => r.Matches))
            {
                Assert.AreEqual(match.Count, (playersOdd.Count - 1) / 2);
            }
        }
        [Test]
        public void Test_EachPlayer_Plays_EachPlayer_Even()
        {
            foreach(var player1 in playersEven)
            {
                foreach(var player2 in playersEven)
                {
                    if (player1 == player2) continue;
                    if (scheduleEven.Rounds.SelectMany(r => r.Matches).FirstOrDefault(m =>
                        m.Players.First == player1 && m.Players.Second == player2 ||
                        m.Players.Second == player1 && m.Players.First == player2 ) is null)
                    {
                        Assert.Fail($"Could not find a matchup between '{player1}' and '{player2}'.");
                    }
                }
            }
        }
        [Test]
        public void Test_EachPlayer_Plays_EachPlayer_Odd()
        {
            foreach (var player1 in playersOdd)
            {
                foreach (var player2 in playersOdd)
                {
                    if (player1 == player2) continue;
                    if (scheduleOdd.Rounds.SelectMany(r => r.Matches).FirstOrDefault(m =>
                        m.Players.First == player1 && m.Players.Second == player2 ||
                        m.Players.Second == player1 && m.Players.First == player2) is null)
                    {
                        Assert.Fail($"Could not find a matchup between '{player1}' and '{player2}'.");
                    }
                }
            }
        }
        [Test]
        public void Test_OddCountPlayers_Skippers()
        {
            foreach (var skipper in scheduleOdd.Rounds.Select(r => r.Skipper))
            {
                Assert.IsNotNull(skipper);
            }
        }
        [Test]
        public void Test_EvenCountPlayers_Skippers()
        {
            foreach(var skipper in scheduleEven.Rounds.Select(r => r.Skipper))
            {
                Assert.IsNull(skipper);
            }
        }
        [Test]
        public void Test_Duplicate_Matchups_Even()
        {
            foreach(var player in playersEven)
            {
                var matches = scheduleEven.Rounds
                    .SelectMany(r => r.Matches)
                    .Where(m => m.Players.First == player || m.Players.Second == player);
                Assert.AreEqual(matches.Count(), playersEven.Count - 1);
            }
        }
        [Test]
        public void Test_Duplicate_Matchups_Odd()
        {
            foreach (var player in playersOdd)
            {
                var matches = scheduleOdd.Rounds
                    .SelectMany(r => r.Matches)
                    .Where(m => m.Players.First == player || m.Players.Second == player);
                Assert.AreEqual(matches.Count(), playersOdd.Count - 1);
            }
        }
    }
}
