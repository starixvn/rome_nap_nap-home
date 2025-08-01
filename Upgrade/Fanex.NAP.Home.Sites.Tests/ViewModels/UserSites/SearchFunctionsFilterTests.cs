using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class SearchFunctionsFilterTests
    {
        [Test]
        public void SearchFunctionsFilter_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var searchedFunctionName = "Test Function";
            var userId = 123;
            var environment = "UAT";

            // Act
            var filter = new SearchFunctionsFilter
            {
                SearchedFunctionName = searchedFunctionName,
                UserId = userId,
                Environment = environment
            };

            // Assert
            Assert.That(filter.SearchedFunctionName, Is.EqualTo(searchedFunctionName));
            Assert.That(filter.UserId, Is.EqualTo(userId));
            Assert.That(filter.Environment, Is.EqualTo(environment));
        }

        [Test]
        public void SearchFunctionsFilter_DefaultValues_AreCorrect()
        {
            // Act
            var filter = new SearchFunctionsFilter();

            // Assert
            Assert.That(filter.SearchedFunctionName, Is.Null);
            Assert.That(filter.UserId, Is.EqualTo(0));
            Assert.That(filter.Environment, Is.Null);
        }

        [Test]
        public void SearchFunctionsFilter_WithNullValues_CanBeSet()
        {
            // Act
            var filter = new SearchFunctionsFilter
            {
                SearchedFunctionName = null,
                UserId = 0,
                Environment = null
            };

            // Assert
            Assert.That(filter.SearchedFunctionName, Is.Null);
            Assert.That(filter.UserId, Is.EqualTo(0));
            Assert.That(filter.Environment, Is.Null);
        }

        [TestCase("Function 1", 1, "UAT")]
        [TestCase("Function 2", 2, "LIVE")]
        [TestCase("Function 3", 3, "DEV")]
        [TestCase("", 0, "")]
        [TestCase(null, 0, null)]
        public void SearchFunctionsFilter_WithDifferentValues_StoresCorrectly(string searchedFunctionName, int userId, string environment)
        {
            // Act
            var filter = new SearchFunctionsFilter
            {
                SearchedFunctionName = searchedFunctionName,
                UserId = userId,
                Environment = environment
            };

            // Assert
            Assert.That(filter.SearchedFunctionName, Is.EqualTo(searchedFunctionName));
            Assert.That(filter.UserId, Is.EqualTo(userId));
            Assert.That(filter.Environment, Is.EqualTo(environment));
        }
    }
} 