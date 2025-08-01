using NUnit.Framework;
using NSubstitute;
using Fanex.NAP.Home.Sites.Services;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Models;
using Fanex.NAP.Home.Sites.Models.UserSites;
using Fanex.NAP.Home.Sites.Criteria.UserSites;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.NAP.Home.Sites.Tests.Services
{
    [TestFixture]
    public class UserSitesServiceTests
    {
        private IDynamicRepository _mockRepository = null!;
        private ConfigurationService _mockConfiguration = null!;
        private IPHelper _mockIpHelper = null!;
        private UserSitesService _service = null!;

        [SetUp]
        public void Setup()
        {
            _mockRepository = Substitute.For<IDynamicRepository>();
            _mockConfiguration = Substitute.For<ConfigurationService>();
            _mockIpHelper = Substitute.For<IPHelper>();
            _service = new UserSitesService(_mockRepository, _mockConfiguration, _mockIpHelper);
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithValidFilter_ReturnsSiteItems()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = "test"
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://test.com", Environment = "UAT", IsDefault = true }
            };

            var menuList = new List<MenuInfo>
            {
                new() { SiteId = 1, MenuItemId = 1, MenuItemName = "Test Menu" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT", "LIVE" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, menuList));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<SiteItem>>());
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithEmptySiteData_ReturnsEmptyList()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((new List<SiteUrlInfo>(), new List<MenuInfo>()));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(0));
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithNullSiteData_ThrowsArgumentNullException()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns(((List<SiteUrlInfo>)null, (List<MenuInfo>)null));

            // Act & Assert
            Assert.That(() => _service.LoadUserSitesAndFunctions(filter), Throws.ArgumentNullException);
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithFilteredEnvironments_FiltersCorrectly()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://test1.com", Environment = "UAT" },
                new() { SiteId = 2, SiteLink = "http://test2.com", Environment = "LIVE" },
                new() { SiteId = 3, SiteLink = "http://test3.com", Environment = "DEV" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT", "LIVE" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, new List<MenuInfo>()));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(2)); // Only UAT and LIVE should be included
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithLocalLinks_SetsIsLocalLinkCorrectly()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://10.0.0.1", Environment = "UAT" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockIpHelper.IsLocalLink("http://10.0.0.1")
                .Returns(true);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, new List<MenuInfo>()));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            _mockIpHelper.Received(1).IsLocalLink("http://10.0.0.1");
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithWebVpnAccess_SetsWebVpnAccessCorrectly()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://test.com", Environment = "UAT" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockIpHelper.IsWebVpnAccess()
                .Returns(true);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, new List<MenuInfo>()));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            _mockIpHelper.Received().IsWebVpnAccess();
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithMultipleSitesAndMenus_GroupsCorrectly()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://site1.com", Environment = "UAT", IsDefault = true },
                new() { SiteId = 1, SiteLink = "http://site1-alt.com", Environment = "UAT", IsDefault = false },
                new() { SiteId = 2, SiteLink = "http://site2.com", Environment = "UAT", IsDefault = true }
            };

            var menuList = new List<MenuInfo>
            {
                new() { SiteId = 1, MenuItemId = 1, MenuItemName = "Menu 1" },
                new() { SiteId = 1, MenuItemId = 2, MenuItemName = "Menu 2" },
                new() { SiteId = 2, MenuItemId = 3, MenuItemName = "Menu 3" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, menuList));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(2)); // Two sites
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithHierarchicalMenus_StructuresCorrectly()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://test.com", Environment = "UAT" }
            };

            var menuList = new List<MenuInfo>
            {
                new() { SiteId = 1, MenuItemId = 1, MenuItemName = "Parent Menu", ParentId = null },
                new() { SiteId = 1, MenuItemId = 2, MenuItemName = "Child Menu", ParentId = 1 },
                new() { SiteId = 1, MenuItemId = 3, MenuItemName = "Grandchild Menu", ParentId = 2 }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, menuList));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
        }

        [Test]
        public void CheckUserPermissionOnFunction_WithValidFilter_ReturnsCommandResult()
        {
            // Arrange
            var filter = new ForwardFilter
            {
                UserId = 1,
                SiteId = 1,
                FunctionId = 1,
                Environment = "UAT"
            };

            _mockRepository.Execute(Arg.Any<CheckPermissionByMenuUserIdCriteria>())
                .Returns(0)
                .AndDoes(x => 
                {
                    var criteria = x.Arg<CheckPermissionByMenuUserIdCriteria>();
                    criteria.ErrCode = 0;
                    criteria.ErrMsg = "Success";
                });

            // Act
            var result = _service.CheckUserPermissionOnFunction(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<CommandResult>());
            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
        public void CheckUserPermissionOnFunction_WithError_ReturnsErrorCommandResult()
        {
            // Arrange
            var filter = new ForwardFilter
            {
                UserId = 1,
                SiteId = 1,
                FunctionId = 1,
                Environment = "UAT"
            };

            _mockRepository.Execute(Arg.Any<CheckPermissionByMenuUserIdCriteria>())
                .Returns(1)
                .AndDoes(x => 
                {
                    var criteria = x.Arg<CheckPermissionByMenuUserIdCriteria>();
                    criteria.ErrCode = 1;
                    criteria.ErrMsg = "Permission denied";
                });

            // Act
            var result = _service.CheckUserPermissionOnFunction(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.Message, Is.EqualTo("Permission denied"));
        }

        [Test]
        public void CheckUserPermissionOnSite_WithValidFilter_ReturnsCommandResult()
        {
            // Arrange
            var filter = new ForwardFilter
            {
                UserId = 1,
                SiteId = 1,
                Environment = "UAT"
            };

            _mockRepository.Execute(Arg.Any<CheckUserPermissionBySiteCriteria>())
                .Returns(0)
                .AndDoes(x => 
                {
                    var criteria = x.Arg<CheckUserPermissionBySiteCriteria>();
                    criteria.ErrCode = 0;
                    criteria.ErrMsg = "Success";
                });

            // Act
            var result = _service.CheckUserPermissionOnSite(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<CommandResult>());
            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
        public void CheckUserPermissionOnSite_WithError_ReturnsErrorCommandResult()
        {
            // Arrange
            var filter = new ForwardFilter
            {
                UserId = 1,
                SiteId = 1,
                Environment = "UAT"
            };

            _mockRepository.Execute(Arg.Any<CheckUserPermissionBySiteCriteria>())
                .Returns(1)
                .AndDoes(x => 
                {
                    var criteria = x.Arg<CheckUserPermissionBySiteCriteria>();
                    criteria.ErrCode = 1;
                    criteria.ErrMsg = "Site access denied";
                });

            // Act
            var result = _service.CheckUserPermissionOnSite(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccessful, Is.False);
            Assert.That(result.Message, Is.EqualTo("Site access denied"));
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithNullSearchedFunctionName_LoadsAllFunctions()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = null
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://test.com", Environment = "UAT" }
            };

            var menuList = new List<MenuInfo>
            {
                new() { SiteId = 1, MenuItemId = 1, MenuItemName = "Menu 1" },
                new() { SiteId = 1, MenuItemId = 2, MenuItemName = "Menu 2" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, menuList));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithEmptySearchedFunctionName_LoadsAllFunctions()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = ""
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://test.com", Environment = "UAT" }
            };

            var menuList = new List<MenuInfo>
            {
                new() { SiteId = 1, MenuItemId = 1, MenuItemName = "Menu 1" },
                new() { SiteId = 1, MenuItemId = 2, MenuItemName = "Menu 2" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, menuList));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
        }

        [Test]
        public void LoadUserSitesAndFunctions_WithNonEmptySearchedFunctionName_LoadsFilteredFunctions()
        {
            // Arrange
            var filter = new SearchFunctionsFilter
            {
                UserId = 1,
                Environment = "UAT",
                SearchedFunctionName = "test"
            };

            var siteUrlList = new List<SiteUrlInfo>
            {
                new() { SiteId = 1, SiteLink = "http://test.com", Environment = "UAT" }
            };

            var menuList = new List<MenuInfo>
            {
                new() { SiteId = 1, MenuItemId = 1, MenuItemName = "Test Menu" },
                new() { SiteId = 1, MenuItemId = 2, MenuItemName = "Other Menu" }
            };

            _mockConfiguration.GetShowSiteEnvironments()
                .Returns(new List<string> { "UAT" });

            _mockIpHelper.IsLocalLink(Arg.Any<string>())
                .Returns(false);

            _mockRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(Arg.Any<GetSiteMenusByUserIdCriteria>())
                .Returns((siteUrlList, menuList));

            // Act
            var result = _service.LoadUserSitesAndFunctions(filter);

            // Assert
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
        }
    }
} 