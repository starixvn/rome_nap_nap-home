using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class ForwardFilterTests
    {
        [Test]
        public void ForwardFilter_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var userId = 123;
            var environment = "UAT";
            var siteId = 1;
            var siteLink = "http://site.com";
            var functionId = 100;
            var functionLink = "/function";

            // Act
            var filter = new ForwardFilter
            {
                UserId = userId,
                Environment = environment,
                SiteId = siteId,
                SiteLink = siteLink,
                FunctionId = functionId,
                FunctionLink = functionLink
            };

            // Assert
            Assert.That(filter.UserId, Is.EqualTo(userId));
            Assert.That(filter.Environment, Is.EqualTo(environment));
            Assert.That(filter.SiteId, Is.EqualTo(siteId));
            Assert.That(filter.SiteLink, Is.EqualTo(siteLink));
            Assert.That(filter.FunctionId, Is.EqualTo(functionId));
            Assert.That(filter.FunctionLink, Is.EqualTo(functionLink));
        }

        [Test]
        public void ForwardFilter_DefaultValues_AreCorrect()
        {
            // Act
            var filter = new ForwardFilter();

            // Assert
            Assert.That(filter.UserId, Is.EqualTo(0));
            Assert.That(filter.Environment, Is.Null);
            Assert.That(filter.SiteId, Is.EqualTo(0));
            Assert.That(filter.SiteLink, Is.Null);
            Assert.That(filter.FunctionId, Is.Null);
            Assert.That(filter.FunctionLink, Is.Null);
        }

        [Test]
        public void ForwardFilter_WithNullValues_CanBeSet()
        {
            // Act
            var filter = new ForwardFilter
            {
                UserId = 0,
                Environment = null,
                SiteId = 0,
                SiteLink = null,
                FunctionId = null,
                FunctionLink = null
            };

            // Assert
            Assert.That(filter.UserId, Is.EqualTo(0));
            Assert.That(filter.Environment, Is.Null);
            Assert.That(filter.SiteId, Is.EqualTo(0));
            Assert.That(filter.SiteLink, Is.Null);
            Assert.That(filter.FunctionId, Is.Null);
            Assert.That(filter.FunctionLink, Is.Null);
        }

        [TestCase(1, "UAT", 1, "http://site1.com", 100, "/function1")]
        [TestCase(2, "LIVE", 2, "http://site2.com", 200, "/function2")]
        [TestCase(3, "DEV", 3, "http://site3.com", 300, "/function3")]
        [TestCase(0, "", 0, "", null, "")]
        [TestCase(0, null, 0, null, null, null)]
        public void ForwardFilter_WithDifferentValues_StoresCorrectly(int userId, string environment, int siteId, string siteLink, int? functionId, string functionLink)
        {
            // Act
            var filter = new ForwardFilter
            {
                UserId = userId,
                Environment = environment,
                SiteId = siteId,
                SiteLink = siteLink,
                FunctionId = functionId,
                FunctionLink = functionLink
            };

            // Assert
            Assert.That(filter.UserId, Is.EqualTo(userId));
            Assert.That(filter.Environment, Is.EqualTo(environment));
            Assert.That(filter.SiteId, Is.EqualTo(siteId));
            Assert.That(filter.SiteLink, Is.EqualTo(siteLink));
            Assert.That(filter.FunctionId, Is.EqualTo(functionId));
            Assert.That(filter.FunctionLink, Is.EqualTo(functionLink));
        }
    }
} 