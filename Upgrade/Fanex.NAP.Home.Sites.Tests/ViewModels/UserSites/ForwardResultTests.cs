using NUnit.Framework;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.NAP.Home.Sites.Models;

namespace Fanex.NAP.Home.Sites.Tests.ViewModels.UserSites
{
    [TestFixture]
    public class ForwardResultTests
    {
        [Test]
        public void ForwardResult_Constructor_WithValidCommandResult_InitializesCorrectly()
        {
            // Arrange
            var commandResult = new CommandResult(0, "Success");

            // Act
            var forwardResult = new ForwardResult(commandResult)
            {
                Link = "http://test.com"
            };

            // Assert
            Assert.That(forwardResult.ErrorCode, Is.EqualTo(0));
            Assert.That(forwardResult.ErrorMessage, Is.EqualTo("Success"));
            Assert.That(forwardResult.Link, Is.EqualTo("http://test.com"));
        }

        [Test]
        public void ForwardResult_Constructor_WithErrorCommandResult_InitializesCorrectly()
        {
            // Arrange
            var commandResult = new CommandResult(500, "Internal Server Error");

            // Act
            var forwardResult = new ForwardResult(commandResult)
            {
                Link = "http://error.com"
            };

            // Assert
            Assert.That(forwardResult.ErrorCode, Is.EqualTo(500));
            Assert.That(forwardResult.ErrorMessage, Is.EqualTo("Internal Server Error"));
            Assert.That(forwardResult.Link, Is.EqualTo("http://error.com"));
        }

        [Test]
        public void ForwardResult_Constructor_WithNullCommandResult_InitializesCorrectly()
        {
            // Act
            var forwardResult = new ForwardResult(null)
            {
                Link = "http://test.com"
            };

            // Assert
            Assert.That(forwardResult.ErrorCode, Is.EqualTo(0));
            Assert.That(forwardResult.ErrorMessage, Is.Null);
            Assert.That(forwardResult.Link, Is.EqualTo("http://test.com"));
        }

        [Test]
        public void ForwardResult_Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var commandResult = new CommandResult(0, "Success");

            // Act
            var forwardResult = new ForwardResult(commandResult)
            {
                Link = "http://example.com"
            };

            // Assert
            Assert.That(forwardResult.Link, Is.EqualTo("http://example.com"));
            Assert.That(forwardResult.ErrorCode, Is.EqualTo(0));
            Assert.That(forwardResult.ErrorMessage, Is.EqualTo("Success"));
        }

        [Test]
        public void ForwardResult_WithNullLink_CanBeSet()
        {
            // Arrange
            var commandResult = new CommandResult(0, "Success");

            // Act
            var forwardResult = new ForwardResult(commandResult)
            {
                Link = null
            };

            // Assert
            Assert.That(forwardResult.Link, Is.Null);
        }

        [Test]
        public void ForwardResult_WithEmptyLink_CanBeSet()
        {
            // Arrange
            var commandResult = new CommandResult(0, "Success");

            // Act
            var forwardResult = new ForwardResult(commandResult)
            {
                Link = ""
            };

            // Assert
            Assert.That(forwardResult.Link, Is.EqualTo(""));
        }

        [TestCase(0, "Success", "http://success.com")]
        [TestCase(404, "Not Found", "http://notfound.com")]
        [TestCase(500, "Server Error", "http://error.com")]
        public void ForwardResult_WithDifferentCommandResults_InitializesCorrectly(int errorCode, string errorMessage, string link)
        {
            // Arrange
            var commandResult = new CommandResult(errorCode, errorMessage);

            // Act
            var forwardResult = new ForwardResult(commandResult)
            {
                Link = link
            };

            // Assert
            Assert.That(forwardResult.ErrorCode, Is.EqualTo(errorCode));
            Assert.That(forwardResult.ErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(forwardResult.Link, Is.EqualTo(link));
        }
    }
} 