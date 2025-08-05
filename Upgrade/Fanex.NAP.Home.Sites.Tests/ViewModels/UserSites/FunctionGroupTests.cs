using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class FunctionGroupTests
    {
        [Test]
        public void FunctionGroup_Constructor_WithValidMenuInfo_InitializesCorrectly()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = 100,
                MenuItemName = "Test Group",
                MenuLink = "/test/group"
            };

            // Act
            var functionGroup = new FunctionGroup((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionGroup.FunctionId, Is.EqualTo(100));
            Assert.That(functionGroup.FunctionName, Is.EqualTo("Test Group"));
            Assert.That(functionGroup.FunctionLink, Is.EqualTo("/test/group"));
            Assert.That(functionGroup.FunctionList, Is.Not.Null);
            Assert.That(functionGroup.FunctionList, Is.Empty);
        }

        [Test]
        public void FunctionGroup_Constructor_WithValidFunctionItem_InitializesCorrectly()
        {
            // Arrange
            var functionItem = new FunctionItem((MenuInfo)new MenuInfo
            {
                MenuItemId = 200,
                MenuItemName = "Source Function",
                MenuLink = "/source/function"
            });

            // Act
            var functionGroup = new FunctionGroup((FunctionItem)functionItem);

            // Assert
            Assert.That(functionGroup.FunctionId, Is.EqualTo(200));
            Assert.That(functionGroup.FunctionName, Is.EqualTo("Source Function"));
            Assert.That(functionGroup.FunctionLink, Is.EqualTo("/source/function"));
            Assert.That(functionGroup.FunctionList, Is.Not.Null);
            Assert.That(functionGroup.FunctionList, Is.Empty);
        }

        [Test]
        public void FunctionGroup_FunctionList_CanBeModified()
        {
            // Arrange
            var functionGroup = new FunctionGroup((MenuInfo)new MenuInfo());
            var functionItem = new FunctionItem((MenuInfo)new MenuInfo
            {
                MenuItemId = 300,
                MenuItemName = "Child Function",
                MenuLink = "/child/function"
            });

            // Act
            functionGroup.FunctionList.Add(functionItem);

            // Assert
            Assert.That(functionGroup.FunctionList.Count, Is.EqualTo(1));
            Assert.That(functionGroup.FunctionList[0], Is.EqualTo(functionItem));
        }

        [Test]
        public void FunctionGroup_FunctionList_CanBeSetToNull()
        {
            // Arrange
            var functionGroup = new FunctionGroup((MenuInfo)new MenuInfo());

            // Act
            functionGroup.FunctionList = null;

            // Assert
            Assert.That(functionGroup.FunctionList, Is.Null);
        }

        [Test]
        public void FunctionGroup_FunctionList_CanBeSetToNewList()
        {
            // Arrange
            var functionGroup = new FunctionGroup((MenuInfo)new MenuInfo());
            var newFunctionList = new List<FunctionItem>
            {
                new FunctionItem((MenuInfo)new MenuInfo { MenuItemId = 400, MenuItemName = "Function 1" }),
                new FunctionItem((MenuInfo)new MenuInfo { MenuItemId = 500, MenuItemName = "Function 2" })
            };

            // Act
            functionGroup.FunctionList = newFunctionList;

            // Assert
            Assert.That(functionGroup.FunctionList, Is.EqualTo(newFunctionList));
            Assert.That(functionGroup.FunctionList.Count, Is.EqualTo(2));
        }

        [Test]
        public void FunctionGroup_InheritsFromFunctionItem()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = 100,
                MenuItemName = "Test Group",
                MenuLink = "/test/group"
            };

            // Act
            var functionGroup = new FunctionGroup((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionGroup, Is.InstanceOf<FunctionItem>());
        }

        [Test]
        public void FunctionGroup_WithNullMenuInfo_InitializesCorrectly()
        {
            // Act
            var functionGroup = new FunctionGroup((MenuInfo)null);

            // Assert
            Assert.That(functionGroup.FunctionId, Is.EqualTo(0));
            Assert.That(functionGroup.FunctionName, Is.Null);
            Assert.That(functionGroup.FunctionLink, Is.Null);
            Assert.That(functionGroup.FunctionList, Is.Not.Null);
            Assert.That(functionGroup.FunctionList, Is.Empty);
        }

        [Test]
        public void FunctionGroup_WithNullFunctionItem_InitializesCorrectly()
        {
            // Act
            var functionGroup = new FunctionGroup((FunctionItem)null);

            // Assert
            Assert.That(functionGroup.FunctionId, Is.EqualTo(0));
            Assert.That(functionGroup.FunctionName, Is.Null);
            Assert.That(functionGroup.FunctionLink, Is.Null);
            Assert.That(functionGroup.FunctionList, Is.Not.Null);
            Assert.That(functionGroup.FunctionList, Is.Empty);
        }
    }
} 