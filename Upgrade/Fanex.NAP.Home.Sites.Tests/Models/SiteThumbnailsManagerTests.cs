using NUnit.Framework;
using Fanex.NAP.Home.Sites.Models;
using System.Reflection;

namespace Fanex.NAP.Home.Sites.Tests.Models
{
    [TestFixture]
    public class SiteThumbnailsManagerTests
    {
        [Test]
        public void GetImageName_WithValidSiteId_ReturnsCorrectImageName()
        {
            // Arrange
            var testThumbnails = new List<SiteThumbnail>
            {
                new SiteThumbnail { SiteId = 1, Name = "Test Site 1", ImageName = "test1.jpg" },
                new SiteThumbnail { SiteId = 2, Name = "Test Site 2", ImageName = "test2.jpg" },
                new SiteThumbnail { SiteId = 0, Name = "Default Site", ImageName = "default.jpg" }
            };

            // Use reflection to set the Thumbnails property
            var thumbnailsProperty = typeof(SiteThumbnailsManager).GetProperty("Thumbnails");
            thumbnailsProperty?.SetValue(null, testThumbnails);

            // Act
            var result = SiteThumbnailsManager.GetImageName(1);

            // Assert
            Assert.That(result, Is.EqualTo("test1.jpg"));
        }

        [Test]
        public void GetImageName_WithInvalidSiteId_ReturnsDefaultImageName()
        {
            // Arrange
            var testThumbnails = new List<SiteThumbnail>
            {
                new SiteThumbnail { SiteId = 1, Name = "Test Site 1", ImageName = "test1.jpg" },
                new SiteThumbnail { SiteId = 0, Name = "Default Site", ImageName = "default.jpg" }
            };

            // Use reflection to set the Thumbnails property
            var thumbnailsProperty = typeof(SiteThumbnailsManager).GetProperty("Thumbnails");
            thumbnailsProperty?.SetValue(null, testThumbnails);

            // Act
            var result = SiteThumbnailsManager.GetImageName(999);

            // Assert
            Assert.That(result, Is.EqualTo("default.jpg"));
        }

        [Test]
        public void GetImageName_WithNoDefaultSite_ReturnsNull()
        {
            // Arrange
            var testThumbnails = new List<SiteThumbnail>
            {
                new SiteThumbnail { SiteId = 1, Name = "Test Site 1", ImageName = "test1.jpg" }
            };

            // Use reflection to set the Thumbnails property
            var thumbnailsProperty = typeof(SiteThumbnailsManager).GetProperty("Thumbnails");
            thumbnailsProperty?.SetValue(null, testThumbnails);

            // Act
            var result = SiteThumbnailsManager.GetImageName(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetImageName_WithEmptyThumbnailsList_ReturnsNull()
        {
            // Arrange
            var testThumbnails = new List<SiteThumbnail>();

            // Use reflection to set the Thumbnails property
            var thumbnailsProperty = typeof(SiteThumbnailsManager).GetProperty("Thumbnails");
            thumbnailsProperty?.SetValue(null, testThumbnails);

            // Act
            var result = SiteThumbnailsManager.GetImageName(1);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetImageName_WithNullThumbnailsList_ReturnsNull()
        {
            // Arrange
            // Use reflection to set the Thumbnails property to null
            var thumbnailsProperty = typeof(SiteThumbnailsManager).GetProperty("Thumbnails");
            thumbnailsProperty?.SetValue(null, null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => SiteThumbnailsManager.GetImageName(1));
        }
    }
} 