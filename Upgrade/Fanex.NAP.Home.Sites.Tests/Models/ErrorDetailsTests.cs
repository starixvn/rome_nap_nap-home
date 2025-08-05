using NUnit.Framework;
using Fanex.NAP.Home.Sites.Models;

namespace Fanex.NAP.Home.Sites.Tests.Models
{
    [TestFixture]
    public class ErrorDetailsTests
    {
        [Test]
        public void ErrorDetails_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var statusCode = 404;
            var message = "Not Found";

            // Act
            var errorDetails = new ErrorDetails
            {
                StatusCode = statusCode,
                Message = message
            };

            // Assert
            Assert.That(errorDetails.StatusCode, Is.EqualTo(statusCode));
            Assert.That(errorDetails.Message, Is.EqualTo(message));
        }

        [Test]
        public void ErrorDetails_DefaultValues_AreCorrect()
        {
            // Act
            var errorDetails = new ErrorDetails();

            // Assert
            Assert.That(errorDetails.StatusCode, Is.EqualTo(0));
            Assert.That(errorDetails.Message, Is.Null);
        }

        [Test]
        public void ToString_WithValidData_ReturnsJsonString()
        {
            // Arrange
            var errorDetails = new ErrorDetails
            {
                StatusCode = 500,
                Message = "Internal Server Error"
            };

            // Act
            var result = errorDetails.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Does.Contain("500"));
            Assert.That(result, Does.Contain("Internal Server Error"));
        }

        [Test]
        public void ToString_WithNullMessage_ReturnsJsonString()
        {
            // Arrange
            var errorDetails = new ErrorDetails
            {
                StatusCode = 400,
                Message = null
            };

            // Act
            var result = errorDetails.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("400"));
        }

        [Test]
        public void ToString_WithEmptyMessage_ReturnsJsonString()
        {
            // Arrange
            var errorDetails = new ErrorDetails
            {
                StatusCode = 200,
                Message = ""
            };

            // Act
            var result = errorDetails.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("200"));
            Assert.That(result, Does.Contain("\"\""));
        }
    }
} 