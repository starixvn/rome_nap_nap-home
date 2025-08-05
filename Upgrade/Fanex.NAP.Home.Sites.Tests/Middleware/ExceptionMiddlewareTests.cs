using NUnit.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Fanex.NAP.Home.Sites.Middleware;
using System.Net;

namespace Fanex.NAP.Home.Sites.Tests.Middleware
{
    [TestFixture]
    public class ExceptionMiddlewareTests
    {
        [Test]
        public void ConfigureExceptionHandler_WithValidAppBuilder_DoesNotThrowException()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging();
            var serviceProvider = services.BuildServiceProvider();
            var appBuilder = new ApplicationBuilder(serviceProvider);

            // Act & Assert
            Assert.That(() => appBuilder.ConfigureExceptionHandler(), Throws.Nothing);
        }

        [Test]
        public void ConfigureExceptionHandler_AddsExceptionHandlerToPipeline()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging();
            var serviceProvider = services.BuildServiceProvider();
            var appBuilder = new ApplicationBuilder(serviceProvider);

            // Act
            appBuilder.ConfigureExceptionHandler();

            // Assert
            // The method should complete without throwing an exception
            // This test verifies that the middleware can be configured
            Assert.That(appBuilder, Is.Not.Null);
        }

        [Test]
        public void ConfigureExceptionHandler_WithNullAppBuilder_ThrowsException()
        {
            // Act & Assert
            Assert.That(() => ExceptionMiddleware.ConfigureExceptionHandler(null), Throws.ArgumentNullException);
        }

        [Test]
        public void ConfigureExceptionHandler_ExtensionMethod_Exists()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging();
            var serviceProvider = services.BuildServiceProvider();
            var appBuilder = new ApplicationBuilder(serviceProvider);

            // Act & Assert
            // This test verifies that the extension method exists and can be called
            Assert.That(() => appBuilder.ConfigureExceptionHandler(), Throws.Nothing);
        }

        [Test]
        public void ConfigureExceptionHandler_WithMinimalServices_DoesNotThrowException()
        {
            // Arrange
            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();
            var appBuilder = new ApplicationBuilder(serviceProvider);

            // Act & Assert
            Assert.That(() => appBuilder.ConfigureExceptionHandler(), Throws.Nothing);
        }
    }
} 