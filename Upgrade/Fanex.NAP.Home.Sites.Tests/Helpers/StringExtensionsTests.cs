using NUnit.Framework;
using Fanex.NAP.Home.Core.Helpers;

namespace Fanex.NAP.Home.Sites.Tests.Helpers
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("Hello World", 5, "He...")]
        [TestCase("Test String", 10, "Test St...")]
        [TestCase("Short", 10, "Short")]
        [TestCase("", 5, "")]
        [TestCase(null, 5, "")]
        public void Truncate_WithDifferentInputs_ReturnsExpectedResult(string input, int length, string expected)
        {
            // Act
            var result = input.Truncate(length);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Truncate_WithNullInput_ReturnsEmpty()
        {
            // Act
            var result = ((string?)null).Truncate(5);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void Truncate_WithEmptyString_ReturnsEmpty()
        {
            // Act
            var result = "".Truncate(5);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void Truncate_WithWhitespaceString_ReturnsEmpty()
        {
            // Act
            var result = "   ".Truncate(5);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [TestCase("  Hello World  ", "Hello World")]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("   ", "")]
        public void TrimOrEmpty_WithDifferentInputs_ReturnsExpectedResult(string input, string expected)
        {
            // Act
            var result = StringExtensions.TrimOrEmpty(input);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TrimOrEmpty_WithNullInput_ReturnsEmptyString()
        {
            // Act
            var result = StringExtensions.TrimOrEmpty(null);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void TrimOrEmpty_WithEmptyInput_ReturnsEmptyString()
        {
            // Act
            var result = StringExtensions.TrimOrEmpty("");

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
} 