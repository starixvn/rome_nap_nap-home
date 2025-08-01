using NUnit.Framework;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.Models.UserSites
{
    [TestFixture]
    public class SitesInfoTests
    {
        [Test]
        public void SitesInfo_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var siteUrlList = new List<SiteUrlInfo>
            {
                new SiteUrlInfo { SiteId = 1, SiteName = "Site 1" },
                new SiteUrlInfo { SiteId = 2, SiteName = "Site 2" }
            };

            var menuList = new List<MenuInfo>
            {
                new MenuInfo { MenuItemId = 1, MenuItemName = "Menu 1" },
                new MenuInfo { MenuItemId = 2, MenuItemName = "Menu 2" }
            };

            // Act
            var sitesInfo = new SitesInfo
            {
                SiteUrlList = siteUrlList,
                MenuList = menuList
            };

            // Assert
            Assert.That(sitesInfo.SiteUrlList, Is.EqualTo(siteUrlList));
            Assert.That(sitesInfo.MenuList, Is.EqualTo(menuList));
        }

        [Test]
        public void SitesInfo_DefaultValues_AreCorrect()
        {
            // Act
            var sitesInfo = new SitesInfo();

            // Assert
            Assert.That(sitesInfo.SiteUrlList, Is.Null);
            Assert.That(sitesInfo.MenuList, Is.Null);
        }

        [Test]
        public void SitesInfo_WithNullCollections_CanBeSet()
        {
            // Act
            var sitesInfo = new SitesInfo
            {
                SiteUrlList = null,
                MenuList = null
            };

            // Assert
            Assert.That(sitesInfo.SiteUrlList, Is.Null);
            Assert.That(sitesInfo.MenuList, Is.Null);
        }

        [Test]
        public void SitesInfo_WithEmptyCollections_CanBeSet()
        {
            // Arrange
            var emptySiteUrlList = new List<SiteUrlInfo>();
            var emptyMenuList = new List<MenuInfo>();

            // Act
            var sitesInfo = new SitesInfo
            {
                SiteUrlList = emptySiteUrlList,
                MenuList = emptyMenuList
            };

            // Assert
            Assert.That(sitesInfo.SiteUrlList, Is.EqualTo(emptySiteUrlList));
            Assert.That(sitesInfo.MenuList, Is.EqualTo(emptyMenuList));
        }
    }
} 