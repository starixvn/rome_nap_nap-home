using NUnit.Framework;
using Fanex.NAP.Home.Sites.Criteria.UserSites;
using Fanex.NAP.Home.Sites.Models.UserSites;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.Criteria.UserSites
{
    [TestFixture]
    public class CreateSiteItemCriteriaTests
    {
        [Test]
        public void CreateSiteItemCriteria_Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo> { new SiteUrlInfo { SiteId = 1, SiteName = "Link 1" } };
            var functionGroups = new List<FunctionGroup> { new FunctionGroup(new MenuInfo { MenuItemId = 100, MenuItemName = "Group 1" }) };
            var isWebVpnAccess = true;

            // Act
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, isWebVpnAccess);

            // Assert
            Assert.That(criteria.SiteInfo, Is.EqualTo(siteInfo));
            Assert.That(criteria.SiteLinks, Is.EqualTo(siteLinks));
            Assert.That(criteria.FunctionGroups, Is.EqualTo(functionGroups));
            Assert.That(criteria.IsWebVpnAccess, Is.EqualTo(isWebVpnAccess));
        }

        [Test]
        public void CreateSiteItemCriteria_Constructor_WithNullValues_InitializesCorrectly()
        {
            // Act
            var criteria = new CreateSiteItemCriteria(null, null, null, false);

            // Assert
            Assert.That(criteria.SiteInfo, Is.Null);
            Assert.That(criteria.SiteLinks, Is.Null);
            Assert.That(criteria.FunctionGroups, Is.Null);
            Assert.That(criteria.IsWebVpnAccess, Is.False);
        }

        [Test]
        public void CreateSiteItemCriteria_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var criteria = new CreateSiteItemCriteria(null, null, null, false);
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo> { new SiteUrlInfo { SiteId = 1, SiteName = "Link 1" } };
            var functionGroups = new List<FunctionGroup> { new FunctionGroup(new MenuInfo { MenuItemId = 100, MenuItemName = "Group 1" }) };

            // Act
            criteria.SiteInfo = siteInfo;
            criteria.SiteLinks = siteLinks;
            criteria.FunctionGroups = functionGroups;
            criteria.IsWebVpnAccess = true;

            // Assert
            Assert.That(criteria.SiteInfo, Is.EqualTo(siteInfo));
            Assert.That(criteria.SiteLinks, Is.EqualTo(siteLinks));
            Assert.That(criteria.FunctionGroups, Is.EqualTo(functionGroups));
            Assert.That(criteria.IsWebVpnAccess, Is.True);
        }

        [Test]
        public void CreateSiteItemCriteria_WithEmptyCollections_InitializesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var emptySiteLinks = new List<SiteUrlInfo>();
            var emptyFunctionGroups = new List<FunctionGroup>();

            // Act
            var criteria = new CreateSiteItemCriteria(siteInfo, emptySiteLinks, emptyFunctionGroups, false);

            // Assert
            Assert.That(criteria.SiteInfo, Is.EqualTo(siteInfo));
            Assert.That(criteria.SiteLinks, Is.EqualTo(emptySiteLinks));
            Assert.That(criteria.FunctionGroups, Is.EqualTo(emptyFunctionGroups));
            Assert.That(criteria.IsWebVpnAccess, Is.False);
        }

        [Test]
        public void CreateSiteItemCriteria_WithMultipleSiteLinks_InitializesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>
            {
                new SiteUrlInfo { SiteId = 1, SiteName = "Link 1", IsLocalLink = true },
                new SiteUrlInfo { SiteId = 1, SiteName = "Link 2", IsLocalLink = false },
                new SiteUrlInfo { SiteId = 1, SiteName = "Link 3", IsLocalLink = false }
            };
            var functionGroups = new List<FunctionGroup>();

            // Act
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, true);

            // Assert
            Assert.That(criteria.SiteInfo, Is.EqualTo(siteInfo));
            Assert.That(criteria.SiteLinks.Count, Is.EqualTo(3));
            Assert.That(criteria.FunctionGroups, Is.EqualTo(functionGroups));
            Assert.That(criteria.IsWebVpnAccess, Is.True);
        }

        [Test]
        public void CreateSiteItemCriteria_WithMultipleFunctionGroups_InitializesCorrectly()
        {
            // Arrange
            var siteInfo = new SiteUrlInfo { SiteId = 1, SiteName = "Test Site" };
            var siteLinks = new List<SiteUrlInfo>();
            var functionGroups = new List<FunctionGroup>
            {
                new FunctionGroup(new MenuInfo { MenuItemId = 100, MenuItemName = "Group 1" }),
                new FunctionGroup(new MenuInfo { MenuItemId = 200, MenuItemName = "Group 2" }),
                new FunctionGroup(new MenuInfo { MenuItemId = 300, MenuItemName = "Group 3" })
            };

            // Act
            var criteria = new CreateSiteItemCriteria(siteInfo, siteLinks, functionGroups, false);

            // Assert
            Assert.That(criteria.SiteInfo, Is.EqualTo(siteInfo));
            Assert.That(criteria.SiteLinks, Is.EqualTo(siteLinks));
            Assert.That(criteria.FunctionGroups.Count, Is.EqualTo(3));
            Assert.That(criteria.IsWebVpnAccess, Is.False);
        }
    }
} 