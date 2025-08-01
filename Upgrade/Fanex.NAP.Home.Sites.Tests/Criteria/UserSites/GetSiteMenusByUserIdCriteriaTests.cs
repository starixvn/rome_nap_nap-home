using NUnit.Framework;
using Fanex.NAP.Home.Sites.Criteria.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.Criteria.UserSites
{
    [TestFixture]
    public class GetSiteMenusByUserIdCriteriaTests
    {
        [Test]
        public void GetSiteMenusByUserIdCriteria_Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            var userId = 123;
            var environment = "UAT";
            var menuItemName = "Test Menu";

            // Act
            var criteria = new GetSiteMenusByUserIdCriteria(userId, environment, menuItemName);

            // Assert
            Assert.That(criteria.UserId, Is.EqualTo(userId));
            Assert.That(criteria.Environment, Is.EqualTo(environment));
            Assert.That(criteria.MenuItemName, Is.EqualTo(menuItemName));
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_GetSettingKey_ReturnsCorrectKey()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(1, "UAT", "Test Menu");

            // Act
            var settingKey = criteria.GetSettingKey();

            // Assert
            Assert.That(settingKey, Is.EqualTo("NAP_Home_MainPage"));
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_IsValid_WithValidUserId_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(123, "UAT", "Test Menu");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_IsValid_WithZeroUserId_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(0, "UAT", "Test Menu");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_IsValid_WithNegativeUserId_ReturnsFalse()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(-1, "UAT", "Test Menu");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_IsValid_WithNullEnvironment_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(123, null, "Test Menu");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_IsValid_WithEmptyEnvironment_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(123, "", "Test Menu");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_IsValid_WithNullMenuItemName_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(123, "UAT", null);

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void GetSiteMenusByUserIdCriteria_IsValid_WithEmptyMenuItemName_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetSiteMenusByUserIdCriteria(123, "UAT", "");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [TestCase(1, "UAT", "Menu 1")]
        [TestCase(100, "LIVE", "Menu 2")]
        [TestCase(999, "DEV", "Menu 3")]
        [TestCase(123, "", "")]
        [TestCase(456, null, null)]
        public void GetSiteMenusByUserIdCriteria_WithDifferentValues_StoresCorrectly(int userId, string environment, string menuItemName)
        {
            // Act
            var criteria = new GetSiteMenusByUserIdCriteria(userId, environment, menuItemName);

            // Assert
            Assert.That(criteria.UserId, Is.EqualTo(userId));
            Assert.That(criteria.Environment, Is.EqualTo(environment));
            Assert.That(criteria.MenuItemName, Is.EqualTo(menuItemName));
        }
    }
} 