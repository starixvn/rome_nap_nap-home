using NUnit.Framework;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.Models.UserSites
{
    [TestFixture]
    public class SiteUrlInfoTests
    {
        [Test]
        public void SiteUrlInfo_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var siteId = 1;
            var siteName = "Test Site";
            var siteLink = "http://test.com";
            var environment = "UAT";
            var isLocalLink = true;
            var isDefault = true;

            // Act
            var siteUrlInfo = new SiteUrlInfo
            {
                SiteId = siteId,
                SiteName = siteName,
                SiteLink = siteLink,
                Environment = environment,
                IsLocalLink = isLocalLink,
                IsDefault = isDefault
            };

            // Assert
            Assert.That(siteUrlInfo.SiteId, Is.EqualTo(siteId));
            Assert.That(siteUrlInfo.SiteName, Is.EqualTo(siteName));
            Assert.That(siteUrlInfo.SiteLink, Is.EqualTo(siteLink));
            Assert.That(siteUrlInfo.Environment, Is.EqualTo(environment));
            Assert.That(siteUrlInfo.IsLocalLink, Is.EqualTo(isLocalLink));
            Assert.That(siteUrlInfo.IsDefault, Is.EqualTo(isDefault));
        }

        [Test]
        public void SiteUrlInfo_DefaultValues_AreCorrect()
        {
            // Act
            var siteUrlInfo = new SiteUrlInfo();

            // Assert
            Assert.That(siteUrlInfo.SiteId, Is.Null);
            Assert.That(siteUrlInfo.SiteName, Is.Null);
            Assert.That(siteUrlInfo.SiteLink, Is.Null);
            Assert.That(siteUrlInfo.Environment, Is.Null);
            Assert.That(siteUrlInfo.IsLocalLink, Is.False);
            Assert.That(siteUrlInfo.IsDefault, Is.Null);
        }

        [Test]
        public void SiteUrlInfo_WithNullValues_CanBeSet()
        {
            // Act
            var siteUrlInfo = new SiteUrlInfo
            {
                SiteId = null,
                SiteName = null,
                SiteLink = null,
                Environment = null,
                IsLocalLink = false,
                IsDefault = null
            };

            // Assert
            Assert.That(siteUrlInfo.SiteId, Is.Null);
            Assert.That(siteUrlInfo.SiteName, Is.Null);
            Assert.That(siteUrlInfo.SiteLink, Is.Null);
            Assert.That(siteUrlInfo.Environment, Is.Null);
            Assert.That(siteUrlInfo.IsLocalLink, Is.False);
            Assert.That(siteUrlInfo.IsDefault, Is.Null);
        }

        [TestCase(1, "Site 1", "http://site1.com", "UAT", true, true)]
        [TestCase(2, "Site 2", "http://site2.com", "LIVE", false, false)]
        [TestCase(3, "Site 3", "http://site3.com", "DEV", true, null)]
        public void SiteUrlInfo_WithDifferentValues_StoresCorrectly(int? siteId, string siteName, string siteLink, string environment, bool isLocalLink, bool? isDefault)
        {
            // Act
            var siteUrlInfo = new SiteUrlInfo
            {
                SiteId = siteId,
                SiteName = siteName,
                SiteLink = siteLink,
                Environment = environment,
                IsLocalLink = isLocalLink,
                IsDefault = isDefault
            };

            // Assert
            Assert.That(siteUrlInfo.SiteId, Is.EqualTo(siteId));
            Assert.That(siteUrlInfo.SiteName, Is.EqualTo(siteName));
            Assert.That(siteUrlInfo.SiteLink, Is.EqualTo(siteLink));
            Assert.That(siteUrlInfo.Environment, Is.EqualTo(environment));
            Assert.That(siteUrlInfo.IsLocalLink, Is.EqualTo(isLocalLink));
            Assert.That(siteUrlInfo.IsDefault, Is.EqualTo(isDefault));
        }
    }
} 