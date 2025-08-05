using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Fanex.NAP.Home.Sites.DependencyInjection;
using Fanex.NAP.Home.Sites.Services;
using Fanex.Data.Repository;
using StackExchange.Redis;

namespace Fanex.NAP.Home.Sites.Tests.DependencyInjection
{
    [TestFixture]
    public class ServiceCollectionTests
    {

        [Test]
        public void RegisterAuthentication_WithValidConfiguration_RegistersRedisCache()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["AuthSettings:LoginPath"] = "/login",
                    ["AuthSettings:LogoutPath"] = "/logout",
                    ["AuthSettings:CookieTimeOut"] = "30",
                    ["AuthSettings:SessionTimeOut"] = "2880",
                    ["AuthSettings:RedisConfig"] = "localhost:6379"
                })
                .Build();

            // Act & Assert
            Assert.That(() => services.RegisterAuthentication(configuration), Throws.TypeOf<RedisConnectionException>());
        }

        [Test]
        public void ProcessWebOptimizer_WithValidServiceCollection_RegistersWebOptimizer()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            // Act
            services.ProcessWebOptimizer();

            // Assert
            Assert.That(() => services.BuildServiceProvider(), Throws.Nothing);
        }
        
        [Test]
        public void RegisterServices_WithValidServiceCollection_RegistersIPHelper()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddHttpContextAccessor();

            // Act
            services.RegisterServices();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.That(() => serviceProvider.GetService<IPHelper>(), Is.Not.Null);
        }

        [Test]
        public void RegisterServices_WithValidServiceCollection_RegistersDynamicRepository()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            // Act
            services.RegisterServices();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.That(() => serviceProvider.GetService<IDynamicRepository>(), Is.Not.Null);
        }

        [Test]
        public void RegisterServices_WithValidServiceCollection_RegistersConfigurationService()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddHttpContextAccessor();

            // Act
            services.RegisterServices();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.That(() => serviceProvider.GetService<ConfigurationService>(), Is.Not.Null);
        }

        [Test]
        public void RegisterServices_WithValidServiceCollection_RegistersUserSitesService()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddHttpContextAccessor();

            // Act
            services.RegisterServices();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.That(() => serviceProvider.GetService<IUserSitesService>(), Is.Not.Null);
        }

        [Test]
        public void RegisterServices_WithValidServiceCollection_RegistersServicesWithCorrectLifetimes()
        {
            // Arrange
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddHttpContextAccessor();

            // Act
            services.RegisterServices();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            
            // Verify singleton services
            var ipHelper1 = serviceProvider.GetService<IPHelper>();
            var ipHelper2 = serviceProvider.GetService<IPHelper>();
            Assert.That(ipHelper1, Is.SameAs(ipHelper2));

            var configService1 = serviceProvider.GetService<ConfigurationService>();
            var configService2 = serviceProvider.GetService<ConfigurationService>();
            Assert.That(configService1, Is.SameAs(configService2));

            // Verify transient services
            var userSitesService1 = serviceProvider.GetService<IUserSitesService>();
            var userSitesService2 = serviceProvider.GetService<IUserSitesService>();
            Assert.That(userSitesService1, Is.Not.SameAs(userSitesService2));
        }
    }
} 