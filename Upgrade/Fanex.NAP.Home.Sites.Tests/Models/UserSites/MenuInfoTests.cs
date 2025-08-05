using NUnit.Framework;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.Models.UserSites
{
    [TestFixture]
    public class MenuInfoTests
    {
        [Test]
        public void MenuInfo_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var siteId = 1;
            var menuItemId = 100;
            var menuItemName = "Test Menu";
            var parentId = 50;
            var menuLink = "/test/menu";
            var displayOrder = (short)1;

            // Act
            var menuInfo = new MenuInfo
            {
                SiteId = siteId,
                MenuItemId = menuItemId,
                MenuItemName = menuItemName,
                ParentId = parentId,
                MenuLink = menuLink,
                DisplayOrder = displayOrder
            };

            // Assert
            Assert.That(menuInfo.SiteId, Is.EqualTo(siteId));
            Assert.That(menuInfo.MenuItemId, Is.EqualTo(menuItemId));
            Assert.That(menuInfo.MenuItemName, Is.EqualTo(menuItemName));
            Assert.That(menuInfo.ParentId, Is.EqualTo(parentId));
            Assert.That(menuInfo.MenuLink, Is.EqualTo(menuLink));
            Assert.That(menuInfo.DisplayOrder, Is.EqualTo(displayOrder));
        }

        [Test]
        public void MenuInfo_DefaultValues_AreCorrect()
        {
            // Act
            var menuInfo = new MenuInfo();

            // Assert
            Assert.That(menuInfo.SiteId, Is.Null);
            Assert.That(menuInfo.MenuItemId, Is.Null);
            Assert.That(menuInfo.MenuItemName, Is.Null);
            Assert.That(menuInfo.ParentId, Is.Null);
            Assert.That(menuInfo.MenuLink, Is.Null);
            Assert.That(menuInfo.DisplayOrder, Is.Null);
        }

        [Test]
        public void MenuInfo_WithNullValues_CanBeSet()
        {
            // Act
            var menuInfo = new MenuInfo
            {
                SiteId = null,
                MenuItemId = null,
                MenuItemName = null,
                ParentId = null,
                MenuLink = null,
                DisplayOrder = null
            };

            // Assert
            Assert.That(menuInfo.SiteId, Is.Null);
            Assert.That(menuInfo.MenuItemId, Is.Null);
            Assert.That(menuInfo.MenuItemName, Is.Null);
            Assert.That(menuInfo.ParentId, Is.Null);
            Assert.That(menuInfo.MenuLink, Is.Null);
            Assert.That(menuInfo.DisplayOrder, Is.Null);
        }

        [TestCase(1, 100, "Menu 1", 50, "/menu1", (short)1)]
        [TestCase(2, 200, "Menu 2", 60, "/menu2", (short)2)]
        [TestCase(3, 300, "Menu 3", 70, "/menu3", (short)3)]
        public void MenuInfo_WithDifferentValues_StoresCorrectly(int? siteId, int? menuItemId, string menuItemName, int? parentId, string menuLink, short? displayOrder)
        {
            // Act
            var menuInfo = new MenuInfo
            {
                SiteId = siteId,
                MenuItemId = menuItemId,
                MenuItemName = menuItemName,
                ParentId = parentId,
                MenuLink = menuLink,
                DisplayOrder = displayOrder
            };

            // Assert
            Assert.That(menuInfo.SiteId, Is.EqualTo(siteId));
            Assert.That(menuInfo.MenuItemId, Is.EqualTo(menuItemId));
            Assert.That(menuInfo.MenuItemName, Is.EqualTo(menuItemName));
            Assert.That(menuInfo.ParentId, Is.EqualTo(parentId));
            Assert.That(menuInfo.MenuLink, Is.EqualTo(menuLink));
            Assert.That(menuInfo.DisplayOrder, Is.EqualTo(displayOrder));
        }
    }
} 