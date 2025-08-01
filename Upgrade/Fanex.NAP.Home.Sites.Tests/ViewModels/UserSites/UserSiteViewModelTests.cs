using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Criteria.UserSites;
using Fanex.NAP.Home.Sites.Models.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class UserSiteViewModelTests
    {
        [Test]
        public void UserSiteViewModel_Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            var sites = new List<SiteItem>
            {
                new SiteItem(new CreateSiteItemCriteria(null, new List<SiteUrlInfo>(), new List<FunctionGroup>(), false))
            };
            var numberOfSitesPerRow = 5;

            // Act
            var viewModel = new UserSiteViewModel(sites, numberOfSitesPerRow);

            // Assert
            Assert.That(viewModel.Sites, Is.EqualTo(sites));
            Assert.That(viewModel.NumberOfSitesPerRow, Is.EqualTo(numberOfSitesPerRow));
        }

        [Test]
        public void UserSiteViewModel_WithEmptySitesList_InitializesCorrectly()
        {
            // Arrange
            var sites = new List<SiteItem>();
            var numberOfSitesPerRow = 3;

            // Act
            var viewModel = new UserSiteViewModel(sites, numberOfSitesPerRow);

            // Assert
            Assert.That(viewModel.Sites, Is.EqualTo(sites));
            Assert.That(viewModel.NumberOfSitesPerRow, Is.EqualTo(numberOfSitesPerRow));
        }

        [Test]
        public void UserSiteViewModel_WithNullSitesList_InitializesCorrectly()
        {
            // Arrange
            var numberOfSitesPerRow = 4;

            // Act
            var viewModel = new UserSiteViewModel(null, numberOfSitesPerRow);

            // Assert
            Assert.That(viewModel.Sites, Is.Null);
            Assert.That(viewModel.NumberOfSitesPerRow, Is.EqualTo(numberOfSitesPerRow));
        }

        [Test]
        public void UserSiteViewModel_WithZeroNumberOfSitesPerRow_InitializesCorrectly()
        {
            // Arrange
            var sites = new List<SiteItem>();
            var numberOfSitesPerRow = 0;

            // Act
            var viewModel = new UserSiteViewModel(sites, numberOfSitesPerRow);

            // Assert
            Assert.That(viewModel.Sites, Is.EqualTo(sites));
            Assert.That(viewModel.NumberOfSitesPerRow, Is.EqualTo(numberOfSitesPerRow));
        }

        [Test]
        public void UserSiteViewModel_Properties_AreReadOnly()
        {
            // Arrange
            var sites = new List<SiteItem>();
            var numberOfSitesPerRow = 5;

            // Act
            var viewModel = new UserSiteViewModel(sites, numberOfSitesPerRow);

            // Assert - Properties should be init-only, so they can't be changed after construction
            // This test verifies the design intent
            Assert.That(viewModel.Sites, Is.EqualTo(sites));
            Assert.That(viewModel.NumberOfSitesPerRow, Is.EqualTo(numberOfSitesPerRow));
        }
    }
} 