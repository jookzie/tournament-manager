using Modules.Entities;
using Modules.Entities.ScheduleTypes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BLL.UnitTests.EntityTests
{
    internal class DoubleRoundRobinTests
    {
        private DoubleRoundRobin scheduleOdd;
        private DoubleRoundRobin scheduleEven;
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
        public DoubleRoundRobinTests()
        {
            scheduleOdd = new DoubleRoundRobin(playersOdd);
            scheduleEven = new DoubleRoundRobin(playersEven);
        }
        [Test]
        public void Test_RoundCount_Even()
        {
            Assert.AreEqual(scheduleEven.Rounds.Count, 2 * (playersEven.Count - 1));
        }
        [Test]
        public void Test_RoundCount_Odd()
        {
            Assert.AreEqual(scheduleOdd.Rounds.Count, 2 * playersOdd.Count);
        }
        [Test]
        public void Test_MatchCount_Even()
        {
            Assert.IsTrue(scheduleEven.Rounds.TrueForAll(r => r.Matches.Count == playersEven.Count / 2));
        }
        [Test]
        public void Test_MatchCount_Odd()
        {
            Assert.IsTrue(scheduleOdd.Rounds.TrueForAll(r => r.Matches.Count == (playersOdd.Count - 1) / 2));
        }
        [Test]
        public void Test_EachPlayer_Plays_EachPlayer_Even()
        {
            foreach (var player1 in playersEven)
            {
                foreach (var player2 in playersEven)
                {
                    if (player1 == player2) continue;
                    if (scheduleEven.Rounds.SelectMany(r => r.Matches).FirstOrDefault(m =>
                        m.Players.First == player1 && m.Players.Second == player2 ||
                        m.Players.Second == player1 && m.Players.First == player2) is null)
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
            Assert.IsTrue(scheduleOdd.Rounds.TrueForAll(r => r.Skipper is not null));
        }
        [Test]
        public void Test_EvenCountPlayers_Skippers()
        {
            Assert.IsTrue(scheduleEven.Rounds.TrueForAll(r => r.Skipper is null));
        }
    }
}
