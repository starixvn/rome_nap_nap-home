using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Fanex.NAP.Home.Sites.Services;

namespace Fanex.NAP.Home.Sites.Tests.Services
{
    [TestFixture]
    public class IPHelperTests
    {
        [TestCase("10.0.0.1", true)]
        [TestCase("172.16.0.1", true)]
        [TestCase("192.168.1.1", true)]
        [TestCase("8.8.8.8", false)]
        [TestCase("google.com", false)]
        public void IsLocalOrVpnRequest_WithDifferentIPs_ReturnsExpectedResult(string ip, bool expected)
        {
            // Arrange
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var ipHelper = new IPHelperImpl(httpContextAccessor);

            // Act
            var result = ipHelper.IsLocalLink($"http://{ip}/test");

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void IsLocalOrVpnRequest_WithEmptyIP_ThrowsException()
        {
            // Arrange
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var ipHelper = new IPHelperImpl(httpContextAccessor);

            // Act & Assert
            Assert.Throws<UriFormatException>(() => ipHelper.IsLocalLink("http:///test"));
        }

        [Test]
        public void IsLocalOrVpnRequest_WithNullIP_ThrowsException()
        {
            // Arrange
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var ipHelper = new IPHelperImpl(httpContextAccessor);

            // Act & Assert
            Assert.Throws<UriFormatException>(() => ipHelper.IsLocalLink("http:///test"));
        }

        [Test]
        public void IsWebVpnAccess_WithLocalHost_ReturnsTrue()
        {
            // Arrange
            var httpContextAccessor = CreateMockHttpContextAccessor("10.0.0.1");
            var ipHelper = new IPHelperImpl(httpContextAccessor);

            // Act
            var result = ipHelper.IsWebVpnAccess();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsWebVpnAccess_WithExternalHost_ReturnsFalse()
        {
            // Arrange
            var httpContextAccessor = CreateMockHttpContextAccessor("google.com");
            var ipHelper = new IPHelperImpl(httpContextAccessor);

            // Act
            var result = ipHelper.IsWebVpnAccess();

            // Assert
            Assert.That(result, Is.False);
        }

        [TestCase("http://10.0.0.1/test", true)]
        [TestCase("https://172.16.1.1/api", true)]
        [TestCase("http://192.168.0.1:8080", true)]
        [TestCase("http://8.8.8.8/test", false)]
        [TestCase("https://google.com/api", false)]
        public void IsLocalLink_WithDifferentUrls_ReturnsExpectedResult(string url, bool expected)
        {
            // Arrange
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var ipHelper = new IPHelperImpl(httpContextAccessor);

            // Act
            var result = ipHelper.IsLocalLink(url);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void IsLocalLink_WithInvalidUrl_ThrowsException()
        {
            // Arrange
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var ipHelper = new IPHelperImpl(httpContextAccessor);

            // Act & Assert
            Assert.Throws<UriFormatException>(() => ipHelper.IsLocalLink("invalid-url"));
        }

        private static IHttpContextAccessor CreateMockHttpContextAccessor(string host = "localhost")
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Host"] = host;

            return new HttpContextAccessor
            {
                HttpContext = httpContext
            };
        }
    }
} 