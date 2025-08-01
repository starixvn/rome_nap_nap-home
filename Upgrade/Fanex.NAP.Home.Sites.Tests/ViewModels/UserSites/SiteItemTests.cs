using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Criteria.UserSites;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class SiteItemTests
    {
        [Test]
        public void SiteItem_Constructor_WithValidCriteria_InitializesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo> { new SiteUrlInfo { SiteId = 1, SiteName = "Link 1", SiteLink = "http://test.com" } };
            var functionGroups = new List<FunctionGroup> { new FunctionGroup(new MenuInfo { MenuItemId = 100, MenuItemName = "Group 1" }) };
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.SiteId, Is.EqualTo(1));
            Assert.That(siteItem.SiteName, Is.EqualTo("Test Site"));
            Assert.That(siteItem.DefaultLink, Is.EqualTo("http://test.com"));
            Assert.That(siteItem.FunctionGroupList, Is.EqualTo(functionGroups));
        }

        [Test]
        public void SiteItem_Constructor_WithNullSiteInfo_InitializesCorrectly()
        {
            // Arrange
            var criteria = new CreateSiteItemCriteria(null, new List<SiteUrlInfo>(), new List<FunctionGroup>(), false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.SiteId, Is.EqualTo(0));
            Assert.That(siteItem.SiteName, Is.Null);
            Assert.That(siteItem.DefaultLink, Is.Null);
            Assert.That(siteItem.Thumbnail, Is.Null);
            Assert.That(siteItem.OtherLinkList, Is.Null);
            Assert.That(siteItem.FunctionGroupList, Is.Not.Null);
            Assert.That(siteItem.FunctionGroupList, Is.Empty);
        }

        [Test]
        public void SiteItem_Constructor_WithNullSiteLinks_InitializesCorrectly()
        {
            // Arrange
            var criteria = new CreateSiteItemCriteria(new SiteUrlInfo(), null, new List<FunctionGroup>(), false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.SiteId, Is.EqualTo(0));
            Assert.That(siteItem.SiteName, Is.Null);
            Assert.That(siteItem.DefaultLink, Is.Null);
            Assert.That(siteItem.Thumbnail, Is.Null);
            Assert.That(siteItem.OtherLinkList, Is.Null);
            Assert.That(siteItem.FunctionGroupList, Is.Not.Null);
            Assert.That(siteItem.FunctionGroupList, Is.Empty);
        }

        [Test]
        public void SiteItem_Constructor_WithEmptySiteLinks_InitializesCorrectly()
        {
            // Arrange
            var criteria = new CreateSiteItemCriteria(new SiteUrlInfo(), new List<SiteUrlInfo>(), new List<FunctionGroup>(), false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.SiteId, Is.EqualTo(0));
            Assert.That(siteItem.SiteName, Is.Null);
            Assert.That(siteItem.DefaultLink, Is.Null);
            Assert.That(siteItem.Thumbnail, Is.Null);
            Assert.That(siteItem.OtherLinkList, Is.Null);
            Assert.That(siteItem.FunctionGroupList, Is.Not.Null);
            Assert.That(siteItem.FunctionGroupList, Is.Empty);
        }

        [Test]
        public void SiteItem_Constructor_WithWebVpnAccessAndLocalLink_UsesLocalLink()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>
            {
                new SiteUrlInfo { SiteId = 1, SiteName = "Local Link", SiteLink = "http://local.com", IsLocalLink = true },
                new SiteUrlInfo { SiteId = 1, SiteName = "External Link", SiteLink = "http://external.com", IsLocalLink = false }
            };
            var functionGroups = new List<FunctionGroup>();
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, true);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.DefaultLink, Is.EqualTo("http://local.com"));
            Assert.That(siteItem.OtherLinkList, Is.Null);
        }

        [Test]
        public void SiteItem_Constructor_WithNonLocalLinks_UsesFirstNonLocalLink()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>
            {
                new SiteUrlInfo { SiteId = 1, SiteName = "Link 1", SiteLink = "http://link1.com", IsLocalLink = false },
                new SiteUrlInfo { SiteId = 1, SiteName = "Link 2", SiteLink = "http://link2.com", IsLocalLink = false },
                new SiteUrlInfo { SiteId = 1, SiteName = "Link 3", SiteLink = "http://link3.com", IsLocalLink = false }
            };
            var functionGroups = new List<FunctionGroup>();
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.DefaultLink, Is.EqualTo("http://link1.com"));
            Assert.That(siteItem.OtherLinkList.Count, Is.EqualTo(2));
            Assert.That(siteItem.OtherLinkList[0], Is.EqualTo("http://link2.com"));
            Assert.That(siteItem.OtherLinkList[1], Is.EqualTo("http://link3.com"));
        }

        [Test]
        public void SiteItem_Constructor_WithMixedLocalAndNonLocalLinks_UsesNonLocalLinks()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>
            {
                new SiteUrlInfo { SiteId = 1, SiteName = "Local Link", SiteLink = "http://local.com", IsLocalLink = true },
                new SiteUrlInfo { SiteId = 1, SiteName = "External Link 1", SiteLink = "http://external1.com", IsLocalLink = false },
                new SiteUrlInfo { SiteId = 1, SiteName = "External Link 2", SiteLink = "http://external2.com", IsLocalLink = false }
            };
            var functionGroups = new List<FunctionGroup>();
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.DefaultLink, Is.EqualTo("http://external1.com"));
            Assert.That(siteItem.OtherLinkList.Count, Is.EqualTo(1));
            Assert.That(siteItem.OtherLinkList[0], Is.EqualTo("http://external2.com"));
        }

        [Test]
        public void SiteItem_Constructor_WithNullFunctionGroups_InitializesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>();
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, null, false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.FunctionGroupList, Is.Not.Null);
            Assert.That(siteItem.FunctionGroupList, Is.Empty);
        }

        [Test]
        public void SiteItem_Constructor_WithEmptyFunctionGroups_InitializesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>();
            var functionGroups = new List<FunctionGroup>();
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.FunctionGroupList, Is.EqualTo(functionGroups));
        }

        [Test]
        public void SiteItem_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var siteItem = new SiteItem(new CreateSiteItemCriteria(null, new List<SiteUrlInfo>(), new List<FunctionGroup>(), false));

            // Act
            siteItem.SiteId = 999;
            siteItem.SiteName = "Custom Site";
            siteItem.DefaultLink = "http://custom.com";
            siteItem.Thumbnail = "custom.jpg";
            siteItem.OtherLinkList = new List<string> { "http://other1.com", "http://other2.com" };

            // Assert
            Assert.That(siteItem.SiteId, Is.EqualTo(999));
            Assert.That(siteItem.SiteName, Is.EqualTo("Custom Site"));
            Assert.That(siteItem.DefaultLink, Is.EqualTo("http://custom.com"));
            Assert.That(siteItem.Thumbnail, Is.EqualTo("custom.jpg"));
            Assert.That(siteItem.OtherLinkList.Count, Is.EqualTo(2));
        }

        [Test]
        public void SiteItem_WithNullSiteLinks_HandlesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var criteria = new CreateSiteItemCriteria(siteInfo, null, new List<FunctionGroup>(), false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.DefaultLink, Is.Null);
            Assert.That(siteItem.OtherLinkList, Is.Null);
        }

        [Test]
        public void SiteItem_WithEmptySiteLinks_HandlesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>();
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, new List<FunctionGroup>(), false);

            // Act
            var siteItem = new SiteItem(criteria);

            // Assert
            Assert.That(siteItem.DefaultLink, Is.Null);
            Assert.That(siteItem.OtherLinkList, Is.Null);
        }
    }
} 