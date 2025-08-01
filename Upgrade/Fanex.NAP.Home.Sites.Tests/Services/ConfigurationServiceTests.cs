using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Fanex.NAP.Home.Sites.Services;

namespace Fanex.NAP.Home.Sites.Tests.Services
{
    [TestFixture]
    public class ConfigurationServiceTests
    {
        [Test]
        public void GetShowSiteEnvironments_WithValidConfiguration_ReturnsCorrectList()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ShowSiteEnvironments"] = "UAT,LIVE,MNL"
                })
                .Build();

            var service = new ConfigurationServiceImp(configuration);

            // Act
            var result = service.GetShowSiteEnvironments();

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result, Does.Contain("UAT"));
            Assert.That(result, Does.Contain("LIVE"));
            Assert.That(result, Does.Contain("MNL"));
        }

        [Test]
        public void GetShowSiteEnvironments_WithEmptyConfiguration_ReturnsListWithEmptyString()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ShowSiteEnvironments"] = ""
                })
                .Build();

            var service = new ConfigurationServiceImp(configuration);

            // Act
            var result = service.GetShowSiteEnvironments();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(""));
        }

        [Test]
        public void UMToken_WithValidConfiguration_ReturnsCorrectValue()
        {
            // Arrange
            var expectedToken = "TestUMToken";
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["UMToken"] = expectedToken
                })
                .Build();

            var service = new ConfigurationServiceImp(configuration);

            // Act
            var result = service.UMToken;

            // Assert
            Assert.That(result, Is.EqualTo(expectedToken));
        }

        [Test]
        public void NumberOfSitesPerRow_WithValidConfiguration_ReturnsCorrectValue()
        {
            // Arrange
            var expectedValue = 5;
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["NumberOfSitesPerRow"] = expectedValue.ToString()
                })
                .Build();

            var service = new ConfigurationServiceImp(configuration);

            // Act
            var result = service.NumberOfSitesPerRow;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void Environment_WithValidConfiguration_ReturnsCorrectValue()
        {
            // Arrange
            var expectedEnvironment = "Live";
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Environment"] = expectedEnvironment
                })
                .Build();

            var service = new ConfigurationServiceImp(configuration);

            // Act
            var result = service.Environment;

            // Assert
            Assert.That(result, Is.EqualTo(expectedEnvironment));
        }

        [Test]
        public void CurrentDirectory_ReturnsAppDomainBaseDirectory()
        {
            // Arrange
            var configuration = new ConfigurationBuilder().Build();
            var service = new ConfigurationServiceImp(configuration);

            // Act
            var result = service.CurrentDirectory;

            // Assert
            Assert.That(result, Is.EqualTo(AppDomain.CurrentDomain.BaseDirectory));
        }
    }
} 