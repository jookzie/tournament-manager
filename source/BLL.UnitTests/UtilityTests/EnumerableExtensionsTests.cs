using Modules.Utilities;
using NUnit.Framework;
using System.Collections.Generic;

namespace BLL.UnitTests.UtilityTests
{
    internal class EnumerableExtensionsTests
    {
        [Test]
        public void Test_FindDuplicates_Valid()
        {
            // Arrange
            var list = new List<int>
            {
                1, 2, 3, 4, 3, 2, 2, 5, 6
            };
            var expectedResult = new Dictionary<int, int>();
            expectedResult.Add(2, 3);
            expectedResult.Add(3, 2);
            // Act
            var actualResult = list.FindDuplicates();
            // Assert
            CollectionAssert.AreEquivalent(actualResult, expectedResult);
        }
        [Test]
        public void Test_FindDuplicates_Invalid()
        {
            // Arrange
            var list = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, -1, -2, -3
            };
            var expectedResult = new Dictionary<int, int>();
            // Act
            var actualResult = list.FindDuplicates();
            // Assert
            CollectionAssert.AreEquivalent(actualResult, expectedResult);
        }
        [Test]
        public void Test_FindDuplicates_Empty()
        {
            // Arrange
            var list = new List<int>();
            var expectedResult = new Dictionary<int, int>();
            // Act
            var actualResult = list.FindDuplicates();
            // Assert
            CollectionAssert.AreEquivalent(actualResult, expectedResult);
        }
    }
}
