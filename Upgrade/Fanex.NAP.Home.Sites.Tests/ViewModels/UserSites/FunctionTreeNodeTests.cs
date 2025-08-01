using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class FunctionTreeNodeTests
    {
        [Test]
        public void FunctionTreeNode_Constructor_WithValidMenuInfo_InitializesCorrectly()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = 100,
                MenuItemName = "Test Node",
                MenuLink = "/test/node"
            };

            // Act
            var functionTreeNode = new FunctionTreeNode((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionTreeNode.FunctionId, Is.EqualTo(100));
            Assert.That(functionTreeNode.FunctionName, Is.EqualTo("Test Node"));
            Assert.That(functionTreeNode.FunctionLink, Is.EqualTo("/test/node"));
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Not.Null);
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Empty);
        }

        [Test]
        public void FunctionTreeNode_Constructor_WithValidFunctionItem_InitializesCorrectly()
        {
            // Arrange
            var functionItem = new FunctionItem((MenuInfo)new MenuInfo
            {
                MenuItemId = 200,
                MenuItemName = "Source Function",
                MenuLink = "/source/function"
            });

            // Act
            var functionTreeNode = new FunctionTreeNode((FunctionItem)functionItem);

            // Assert
            Assert.That(functionTreeNode.FunctionId, Is.EqualTo(200));
            Assert.That(functionTreeNode.FunctionName, Is.EqualTo("Source Function"));
            Assert.That(functionTreeNode.FunctionLink, Is.EqualTo("/source/function"));
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Not.Null);
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Empty);
        }

        [Test]
        public void FunctionTreeNode_ChildFunctionNodeList_CanBeModified()
        {
            // Arrange
            var functionTreeNode = new FunctionTreeNode((MenuInfo)new MenuInfo());
            var childNode = new FunctionTreeNode((MenuInfo)new MenuInfo
            {
                MenuItemId = 300,
                MenuItemName = "Child Node",
                MenuLink = "/child/node"
            });

            // Act
            functionTreeNode.ChildFunctionNodeList.Add(childNode);

            // Assert
            Assert.That(functionTreeNode.ChildFunctionNodeList.Count, Is.EqualTo(1));
            Assert.That(functionTreeNode.ChildFunctionNodeList[0], Is.EqualTo(childNode));
        }

        [Test]
        public void FunctionTreeNode_ChildFunctionNodeList_CanBeSetToNull()
        {
            // Arrange
            var functionTreeNode = new FunctionTreeNode((MenuInfo)new MenuInfo());

            // Act
            functionTreeNode.ChildFunctionNodeList = null;

            // Assert
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Null);
        }

        [Test]
        public void FunctionTreeNode_ChildFunctionNodeList_CanBeSetToNewList()
        {
            // Arrange
            var functionTreeNode = new FunctionTreeNode((MenuInfo)new MenuInfo());
            var newChildList = new List<FunctionTreeNode>
            {
                new FunctionTreeNode((MenuInfo)new MenuInfo { MenuItemId = 400, MenuItemName = "Node 1" }),
                new FunctionTreeNode((MenuInfo)new MenuInfo { MenuItemId = 500, MenuItemName = "Node 2" })
            };

            // Act
            functionTreeNode.ChildFunctionNodeList = newChildList;

            // Assert
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.EqualTo(newChildList));
            Assert.That(functionTreeNode.ChildFunctionNodeList.Count, Is.EqualTo(2));
        }

        [Test]
        public void FunctionTreeNode_InheritsFromFunctionItem()
        {
            // Arrange
            var menuInfo = new MenuInfo
            {
                MenuItemId = 100,
                MenuItemName = "Test Node",
                MenuLink = "/test/node"
            };

            // Act
            var functionTreeNode = new FunctionTreeNode((MenuInfo)menuInfo);

            // Assert
            Assert.That(functionTreeNode, Is.InstanceOf<FunctionItem>());
        }

        [Test]
        public void FunctionTreeNode_WithNullMenuInfo_InitializesCorrectly()
        {
            // Act
            var functionTreeNode = new FunctionTreeNode((MenuInfo)null);

            // Assert
            Assert.That(functionTreeNode.FunctionId, Is.EqualTo(0));
            Assert.That(functionTreeNode.FunctionName, Is.Null);
            Assert.That(functionTreeNode.FunctionLink, Is.Null);
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Not.Null);
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Empty);
        }

        [Test]
        public void FunctionTreeNode_WithNullFunctionItem_InitializesCorrectly()
        {
            // Act
            var functionTreeNode = new FunctionTreeNode((FunctionItem)null);

            // Assert
            Assert.That(functionTreeNode.FunctionId, Is.EqualTo(0));
            Assert.That(functionTreeNode.FunctionName, Is.Null);
            Assert.That(functionTreeNode.FunctionLink, Is.Null);
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Not.Null);
            Assert.That(functionTreeNode.ChildFunctionNodeList, Is.Empty);
        }

        [Test]
        public void FunctionTreeNode_CanHaveNestedChildren()
        {
            // Arrange
            var parentNode = new FunctionTreeNode((MenuInfo)new MenuInfo { MenuItemId = 100, MenuItemName = "Parent" });
            var childNode = new FunctionTreeNode((MenuInfo)new MenuInfo { MenuItemId = 200, MenuItemName = "Child" });
            var grandChildNode = new FunctionTreeNode((MenuInfo)new MenuInfo { MenuItemId = 300, MenuItemName = "GrandChild" });

            // Act
            childNode.ChildFunctionNodeList.Add(grandChildNode);
            parentNode.ChildFunctionNodeList.Add(childNode);

            // Assert
            Assert.That(parentNode.ChildFunctionNodeList.Count, Is.EqualTo(1));
            Assert.That(parentNode.ChildFunctionNodeList[0].ChildFunctionNodeList.Count, Is.EqualTo(1));
            Assert.That(parentNode.ChildFunctionNodeList[0].ChildFunctionNodeList[0].FunctionName, Is.EqualTo("GrandChild"));
        }
    }
} 