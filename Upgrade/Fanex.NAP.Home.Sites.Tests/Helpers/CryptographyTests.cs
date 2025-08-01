using NUnit.Framework;
using Fanex.Common;

namespace Fanex.NAP.Home.Sites.Tests.Helpers
{
    [TestFixture]
    public class CryptographyTests
    {
        [Test]
        public void CfsEnCrypt_WithValidString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "test123";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithEmptyString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithNullString_ThrowsArgumentNullException()
        {
            // Arrange
            string plainText = null;

            // Act & Assert
            Assert.That(() => Cryptography.CfsEnCrypt(plainText), Throws.ArgumentNullException);
        }

        [Test]
        public void CfsEnCrypt_WithSpecialCharacters_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "!@#$%^&*()_+-=[]{}|;':\",./<>?";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithUnicodeCharacters_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "Hello 世界 🌍";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithLongString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = new string('a', 1000);

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithSameInput_ReturnsConsistentResult()
        {
            // Arrange
            var plainText = "test123";

            // Act
            var result1 = Cryptography.CfsEnCrypt(plainText);
            var result2 = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result1, Is.EqualTo(result2));
        }

        [Test]
        public void CfsEnCrypt_WithDifferentInputs_ReturnsDifferentResults()
        {
            // Arrange
            var plainText1 = "test123";
            var plainText2 = "test124";

            // Act
            var result1 = Cryptography.CfsEnCrypt(plainText1);
            var result2 = Cryptography.CfsEnCrypt(plainText2);

            // Assert
            Assert.That(result1, Is.Not.EqualTo(result2));
        }

        [Test]
        public void CfsEnCrypt_WithNumericString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "123456789";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithSingleCharacter_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "A";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithWhitespaceString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "   ";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithNewlineCharacters_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "line1\nline2\r\nline3";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithTabCharacters_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "column1\tcolumn2\tcolumn3";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithMixedCaseString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "HelloWorld123";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithUrlString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "https://example.com/path?param=value#fragment";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithJsonString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "{\"name\":\"John\",\"age\":30,\"city\":\"New York\"}";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithXmlString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "<root><element>value</element></root>";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithBase64String_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "SGVsbG8gV29ybGQ=";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithHexadecimalString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "48656C6C6F20576F726C64";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithBinaryString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "1010101010101010";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithRepeatedCharacters_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "aaaaaaaaaaaaaaaaaaaa";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithPalindromeString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "racecar";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void CfsEnCrypt_WithRandomString_ReturnsEncryptedString()
        {
            // Arrange
            var plainText = "xK9mP2qR8vN4wL7sA1cE6tY3uI5oP9";

            // Act
            var result = Cryptography.CfsEnCrypt(plainText);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Not.EqualTo(plainText));
        }
    }
} 