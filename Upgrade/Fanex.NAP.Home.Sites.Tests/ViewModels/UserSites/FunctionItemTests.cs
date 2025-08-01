using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class FunctionItemTests
    {
        [Test]
        public void FunctionItem_Constructor_WithValidMenuInfo_InitializesCorrectly()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = 100,
                MenuItemName = "Test Function",
                MenuLink = "  /test/function  "
            };

            // Act
            var functionItem = new FunctionItem((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionItem.FunctionId, Is.EqualTo(100));
            Assert.That(functionItem.FunctionName, Is.EqualTo("Test Function"));
            Assert.That(functionItem.FunctionLink, Is.EqualTo("/test/function"));
            Assert.That(functionItem.DisplayOrder, Is.EqualTo(0));
        }

        [Test]
        public void FunctionItem_Constructor_WithNullMenuInfo_InitializesCorrectly()
        {
            // Act
            var functionItem = new FunctionItem((MenuInfo)null);

            // Assert
            Assert.That(functionItem.FunctionId, Is.EqualTo(0));
            Assert.That(functionItem.FunctionName, Is.Null);
            Assert.That(functionItem.FunctionLink, Is.Null);
            Assert.That(functionItem.DisplayOrder, Is.EqualTo(0));
        }

        [Test]
        public void FunctionItem_Constructor_WithMenuInfoWithoutMenuItemId_InitializesCorrectly()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = null,
                MenuItemName = "Test Function",
                MenuLink = "/test/function"
            };

            // Act
            var functionItem = new FunctionItem((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionItem.FunctionId, Is.EqualTo(0));
            Assert.That(functionItem.FunctionName, Is.Null);
            Assert.That(functionItem.FunctionLink, Is.Null);
            Assert.That(functionItem.DisplayOrder, Is.EqualTo(0));
        }

        [Test]
        public void FunctionItem_Constructor_WithValidFunctionItem_InitializesCorrectly()
        {
            // Arrange
            var sourceFunctionItem = new FunctionItem((MenuInfo)new MenuInfo
            {
                MenuItemId = 200,
                MenuItemName = "Source Function",
                MenuLink = "/source/function"
            })
            {
                DisplayOrder = 5
            };

            // Act
            var functionItem = new FunctionItem((FunctionItem)sourceFunctionItem);

            // Assert
            Assert.That(functionItem.FunctionId, Is.EqualTo(200));
            Assert.That(functionItem.FunctionName, Is.EqualTo("Source Function"));
            Assert.That(functionItem.FunctionLink, Is.EqualTo("/source/function"));
            Assert.That(functionItem.DisplayOrder, Is.EqualTo(0)); // DisplayOrder is not copied in constructor
        }

        [Test]
        public void FunctionItem_Constructor_WithNullFunctionItem_InitializesCorrectly()
        {
            // Act
            var functionItem = new FunctionItem((FunctionItem)null);

            // Assert
            Assert.That(functionItem.FunctionId, Is.EqualTo(0));
            Assert.That(functionItem.FunctionName, Is.Null);
            Assert.That(functionItem.FunctionLink, Is.Null);
            Assert.That(functionItem.DisplayOrder, Is.EqualTo(0));
        }

        [Test]
        public void FunctionItem_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var functionItem = new FunctionItem((MenuInfo)new MenuInfo());

            // Act
            functionItem.FunctionId = 300;
            functionItem.FunctionName = "Custom Function";
            functionItem.FunctionLink = "/custom/function";
            functionItem.DisplayOrder = 10;

            // Assert
            Assert.That(functionItem.FunctionId, Is.EqualTo(300));
            Assert.That(functionItem.FunctionName, Is.EqualTo("Custom Function"));
            Assert.That(functionItem.FunctionLink, Is.EqualTo("/custom/function"));
            Assert.That(functionItem.DisplayOrder, Is.EqualTo(10));
        }

        [Test]
        public void FunctionItem_WithEmptyMenuLink_TrimsCorrectly()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = 100,
                MenuItemName = "Test Function",
                MenuLink = ""
            };

            // Act
            var functionItem = new FunctionItem((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionItem.FunctionLink, Is.EqualTo(""));
        }

        [Test]
        public void FunctionItem_WithNullMenuLink_HandlesCorrectly()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = 100,
                MenuItemName = "Test Function",
                MenuLink = null
            };

            // Act
            var functionItem = new FunctionItem((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionItem.FunctionLink, Is.EqualTo(""));
        }
    }
} 