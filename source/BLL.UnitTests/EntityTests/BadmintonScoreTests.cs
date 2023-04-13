using Modules.Entities;
using NUnit.Framework;
using System;

namespace BLL.UnitTests.EntityTests
{
    internal class BadmintonScoreTests
    {
        [Test]
        [TestCase(0, 21)]
        [TestCase(1, 21)]
        [TestCase(19, 21)]
        [TestCase(18, 21)]
        [TestCase(20, 22)]
        [TestCase(25, 27)]
        [TestCase(28, 30)]
        [TestCase(29, 30)]
        [TestCase(30, 29)]
        public void Test_Valid_Scores(int score1, int score2)
        {
            new Game((new User(), new User()))
            {
                Scores = (score1, score2)
            };
        }
        
        [Test]
        [TestCase(-1, 21)]
        [TestCase(-1, -1)]
        [TestCase(01, 01)]
        [TestCase(20, 20)]
        [TestCase(20, 21)]
        [TestCase(20, 23)]
        [TestCase(21, 24)]
        [TestCase(30, 30)]
        [TestCase(31, 30)]
        [TestCase(31, 31)]
        public void Test_Invalid_Scores(int score1, int score2)
        {
            Assert.Throws<ArgumentException>(() =>
            new Game((new User(), new User()))
            {
                Scores = (score1, score2)
            });
        }
    }
}
