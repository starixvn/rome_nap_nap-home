using NUnit.Framework;
using Fanex.NAP.Home.Sites.Models;

namespace Fanex.NAP.Home.Sites.Tests.Models
{
    [TestFixture]
    public class SiteThumbnailTests
    {
        [Test]
        public void SiteThumbnail_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var siteId = 123;
            var name = "Test Site";
            var imageName = "test-site.jpg";

            // Act
            var thumbnail = new SiteThumbnail
            {
                SiteId = siteId,
                Name = name,
                ImageName = imageName
            };

            // Assert
            Assert.That(thumbnail.SiteId, Is.EqualTo(siteId));
            Assert.That(thumbnail.Name, Is.EqualTo(name));
            Assert.That(thumbnail.ImageName, Is.EqualTo(imageName));
        }

        [Test]
        public void SiteThumbnail_DefaultValues_AreCorrect()
        {
            // Act
            var thumbnail = new SiteThumbnail();

            // Assert
            Assert.That(thumbnail.SiteId, Is.EqualTo(0));
            Assert.That(thumbnail.Name, Is.Null);
            Assert.That(thumbnail.ImageName, Is.Null);
        }

        [TestCase(0, "Default Site", "default.jpg")]
        [TestCase(1, "Site 1", "site1.jpg")]
        [TestCase(999, "Large Site", "large-site.jpg")]
        public void SiteThumbnail_WithDifferentValues_StoresCorrectly(int siteId, string name, string imageName)
        {
            // Act
            var thumbnail = new SiteThumbnail
            {
                SiteId = siteId,
                Name = name,
                ImageName = imageName
            };

            // Assert
            Assert.That(thumbnail.SiteId, Is.EqualTo(siteId));
            Assert.That(thumbnail.Name, Is.EqualTo(name));
            Assert.That(thumbnail.ImageName, Is.EqualTo(imageName));
        }
    }
} 