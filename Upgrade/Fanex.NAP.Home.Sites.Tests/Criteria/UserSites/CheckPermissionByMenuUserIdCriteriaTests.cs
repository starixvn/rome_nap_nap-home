using NUnit.Framework;
using Fanex.NAP.Home.Sites.Criteria.UserSites;

namespace Fanex.NAP.Home.Sites.Tests.Criteria.UserSites
{
    [TestFixture]
    public class CheckPermissionByMenuUserIdCriteriaTests
    {
        [Test]
        public void CheckPermissionByMenuUserIdCriteria_Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            var userId = 123;
            var siteId = 1;
            var menuId = 100;
            var environment = "UAT";

            // Act
            var criteria = new CheckPermissionByMenuUserIdCriteria(userId, siteId, menuId, environment);

            // Assert
            Assert.That(criteria.UserId, Is.EqualTo(userId));
            Assert.That(criteria.SiteId, Is.EqualTo(siteId));
            Assert.That(criteria.MenuId, Is.EqualTo(menuId));
            Assert.That(criteria.Environment, Is.EqualTo(environment));
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_GetSettingKey_ReturnsCorrectKey()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(1, 1, 1, "UAT");

            // Act
            var settingKey = criteria.GetSettingKey();

            // Assert
            Assert.That(settingKey, Is.EqualTo("NAP_Home_CheckUserPermissionByMenu"));
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithValidData_ReturnsTrue()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(123, 1, 100, "UAT");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithZeroUserId_ReturnsFalse()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(0, 1, 100, "UAT");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithNegativeUserId_ReturnsFalse()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(-1, 1, 100, "UAT");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithZeroSiteId_ReturnsFalse()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(123, 0, 100, "UAT");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithZeroMenuId_ReturnsFalse()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(123, 1, 0, "UAT");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithNullEnvironment_ReturnsFalse()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(123, 1, 100, null);

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithEmptyEnvironment_ReturnsFalse()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(123, 1, 100, "");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_IsValid_WithWhitespaceEnvironment_ReturnsTrue()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(1, 1, 1, "   ");

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_ErrMsgAndErrCode_CanBeSetAndRetrieved()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(123, 1, 100, "UAT");

            // Act
            criteria.ErrMsg = "Test Error Message";
            criteria.ErrCode = 500;

            // Assert
            Assert.That(criteria.ErrMsg, Is.EqualTo("Test Error Message"));
            Assert.That(criteria.ErrCode, Is.EqualTo(500));
        }

        [Test]
        public void CheckPermissionByMenuUserIdCriteria_DefaultErrValues_AreCorrect()
        {
            // Arrange
            var criteria = new CheckPermissionByMenuUserIdCriteria(123, 1, 100, "UAT");

            // Assert
            Assert.That(criteria.ErrMsg, Is.Null);
            Assert.That(criteria.ErrCode, Is.EqualTo(0));
        }
    }
} 