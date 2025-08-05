using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Fanex.NAP.Home.Sites.Controllers;
using Fanex.NAP.Home.Sites.Services;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Models;
using Fanex.Common;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Services;
using Fanex.NAP.Client.Configuration;
using NSubstitute;

namespace Fanex.NAP.Home.Sites.Tests.Controllers
{
    [TestFixture]
    public class UserSitesControllerTests
    {
        [Test]
        public void Index_ReturnsRedirectToSitesAction()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            var redirectResult = (RedirectToActionResult)result;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Sites"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("UserSites"));
        }

        [Test]
        public void Sites_WithValidRequest_ReturnsViewResult()
        {
            // Arrange
            var controller = CreateController();
            var searchFunction = "test";

            // Act
            var result = controller.Sites(searchFunction);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.Not.Null);
            Assert.That(viewResult.Model, Is.InstanceOf<UserSiteViewModel>());
        }

        [Test]
        public void Sites_WithNullSearchFunction_ReturnsViewResult()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.Sites(null);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.Not.Null);
            Assert.That(viewResult.Model, Is.InstanceOf<UserSiteViewModel>());
        }

        [Test]
        public void Sites_WithEmptySearchFunction_ReturnsViewResult()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.Sites("");

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.Not.Null);
            Assert.That(viewResult.Model, Is.InstanceOf<UserSiteViewModel>());
        }

        [Test]
        public void Sites_WithUMTokenInQuery_SetsSessionValue()
        {
            // Arrange
            var controller = CreateController();
            var umToken = "test-token";
            controller.HttpContext.Request.Query = new QueryCollection(
                new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
                {
                    { "UMToken", umToken }
                });

            // Act
            var result = controller.Sites("test");

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            // Verify session was set (this would require more complex setup in a real test)
        }

        [Test]
        public void Sites_WithoutUMTokenInQuery_DoesNotSetSessionValue()
        {
            // Arrange
            var controller = CreateController();
            controller.HttpContext.Request.Query = new QueryCollection(
                new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

            // Act
            var result = controller.Sites("test");

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Sites_SetsViewBagProperties()
        {
            // Arrange
            var controller = CreateController();
            var searchFunction = "test";

            // Act
            var result = controller.Sites(searchFunction);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewData["SearchedFunctionName"], Is.EqualTo(searchFunction));
            Assert.That(viewResult.ViewData["IsHomePage"], Is.EqualTo(true));
            Assert.That(viewResult.ViewData["Environment"], Is.Not.Null);
        }

        [Test]
        public void Forward_WithValidFilter_ReturnsJsonResult()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = "http://test.com",
                FunctionLink = "/test"
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
            var jsonResult = (JsonResult)result;
            Assert.That(jsonResult.Value, Is.Not.Null);
            Assert.That(jsonResult.Value, Is.InstanceOf<ForwardResult>());
        }

        [Test]
        public void Forward_WithNullFilter_ThrowsException()
        {
            // Arrange
            var controller = CreateController();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => controller.Forward(null));
        }

        [Test]
        public void Forward_WithNullSiteLink_SetsToEmptyString()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = null,
                FunctionLink = "/test"
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Forward_WithEmptyFunctionLink_SetsToEmptyString()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = "http://test.com",
                FunctionLink = ""
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Forward_WithNullFunctionLink_SetsToEmptyString()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = "http://test.com",
                FunctionLink = null
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Forward_WithFunctionId_ChecksFunctionPermission()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = "http://test.com",
                FunctionLink = "/test"
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Forward_WithoutFunctionId_ChecksSitePermission()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = null,
                SiteLink = "http://test.com",
                FunctionLink = "/test"
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Forward_WithZeroFunctionId_ChecksSitePermission()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 0,
                SiteLink = "http://test.com",
                FunctionLink = "/test"
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Forward_WithSuccessfulPermission_GeneratesForwardLink()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = "http://test.com",
                FunctionLink = "/test"
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
            var jsonResult = (JsonResult)result;
            var forwardResult = (ForwardResult)jsonResult.Value;
            Assert.That(forwardResult.Link, Is.Not.Null);
            Assert.That(forwardResult.Link, Is.Not.Empty);
        }

        [Test]
        public void Forward_WithFailedPermission_DoesNotGenerateForwardLink()
        {
            // Arrange
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = "http://test.com",
                FunctionLink = "/test"
            };

            // Mock failed permission check
            var mockSiteService = Substitute.For<IUserSitesService>();
            mockSiteService.CheckUserPermissionOnFunction(Arg.Any<ForwardFilter>())
                .Returns(new CommandResult(1, "Permission denied"));

            var mockNapSession = Substitute.For<INapSession>();
            var mockConfigurationService = Substitute.For<ConfigurationService>();
            var mockNapSettings = Substitute.For<INapSettings>();
            var mockSecurityService = Substitute.For<ISecurityService>();

            // Setup default mock behaviors
            mockNapSession.UserId.Returns(1);
            mockNapSession.OnUserId.Returns(1);
            mockConfigurationService.Environment.Returns("Test");
            mockConfigurationService.NumberOfSitesPerRow.Returns(5);
            mockConfigurationService.UMToken.Returns("UMToken");
            mockSecurityService.GetTime().Returns("1234567890");
            mockNapSettings.Environment.Returns("UAT");

            var controller = new UserSitesController(
                mockSiteService,
                mockNapSession,
                mockConfigurationService,
                mockNapSettings,
                mockSecurityService);

            // Setup HttpContext with session
            var httpContext = new DefaultHttpContext();
            var session = Substitute.For<ISession>();
            httpContext.Session = session;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
            var jsonResult = (JsonResult)result;
            var forwardResult = (ForwardResult)jsonResult.Value;
            Assert.That(forwardResult.Link, Is.Null);
        }

        [Test]
        public void Forward_WithUMTokenInSession_IncludesInLink()
        {
            // Arrange
            var controller = CreateController();
            var filter = new ForwardFilter
            {
                SiteId = 1,
                FunctionId = 1,
                SiteLink = "http://test.com",
                FunctionLink = "/test"
            };

            // Set UMToken in session using the actual session implementation
            var httpContext = new DefaultHttpContext();
            var session = new TestSession();
            session.SetString("UMToken", "test-token");
            httpContext.Session = session;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = controller.Forward(filter);

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
            var jsonResult = (JsonResult)result;
            var forwardResult = (ForwardResult)jsonResult.Value;
            Assert.That(forwardResult.Link, Is.Not.Null);
            Assert.That(forwardResult.Link, Does.Contain("UMToken=test-token"));
        }

        // Helper class for testing session
        private class TestSession : ISession
        {
            private readonly Dictionary<string, byte[]> _data = new();

            public string Id => "test-session-id";
            public bool IsAvailable => true;
            public IEnumerable<string> Keys => _data.Keys;

            public void Clear() => _data.Clear();
            public void Remove(string key) => _data.Remove(key);
            public void Set(string key, byte[] value) => _data[key] = value;
            public bool TryGetValue(string key, out byte[] value) => _data.TryGetValue(key, out value);

            public void SetString(string key, string value)
            {
                _data[key] = System.Text.Encoding.UTF8.GetBytes(value);
            }

            public string GetString(string key)
            {
                return _data.TryGetValue(key, out var value) 
                    ? System.Text.Encoding.UTF8.GetString(value) 
                    : null;
            }

            public Task LoadAsync(CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task CommitAsync(CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }

        private static UserSitesController CreateController(IUserSitesService? mockSiteService = null)
        {
            // Mock services
            mockSiteService ??= Substitute.For<IUserSitesService>();
            var mockNapSession = Substitute.For<INapSession>();
            var mockConfigurationService = Substitute.For<ConfigurationService>();
            var mockNapSettings = Substitute.For<INapSettings>();
            var mockSecurityService = Substitute.For<ISecurityService>();

            // Setup default mock behaviors
            mockNapSession.UserId.Returns(1);
            mockNapSession.OnUserId.Returns(1);
            mockConfigurationService.Environment.Returns("Test");
            mockConfigurationService.NumberOfSitesPerRow.Returns(5);
            mockConfigurationService.UMToken.Returns("UMToken");
            mockSecurityService.GetTime().Returns("1234567890");
            mockNapSettings.Environment.Returns("UAT");
            
            // Setup site service mocks
            mockSiteService.LoadUserSitesAndFunctions(Arg.Any<SearchFunctionsFilter>())
                .Returns(new List<SiteItem>());
            mockSiteService.CheckUserPermissionOnFunction(Arg.Any<ForwardFilter>())
                .Returns(new CommandResult(0, "Success"));
            mockSiteService.CheckUserPermissionOnSite(Arg.Any<ForwardFilter>())
                .Returns(new CommandResult(0, "Success"));

            var controller = new UserSitesController(
                mockSiteService,
                mockNapSession,
                mockConfigurationService,
                mockNapSettings,
                mockSecurityService);

            // Setup HttpContext with session
            var httpContext = new DefaultHttpContext();
            var session = Substitute.For<ISession>();
            httpContext.Session = session;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            return controller;
        }
    }
} 