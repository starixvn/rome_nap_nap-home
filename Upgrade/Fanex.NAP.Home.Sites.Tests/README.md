# Fanex.NAP.Home.Sites.Tests

This is the unit test project for the Fanex.NAP.Home.Sites application. The project has been converted from xUnit to NUnit framework.

## Project Structure

```
Fanex.NAP.Home.Sites.Tests/
├── Controllers/
│   └── UserSitesControllerTests.cs
├── Helpers/
│   ├── CryptographyTests.cs
│   └── StringExtensionsTests.cs
├── Models/
│   ├── SiteThumbnailTests.cs
│   └── SiteThumbnailsManagerTests.cs
├── Services/
│   ├── ConfigurationServiceTests.cs
│   └── IPHelperTests.cs
└── README.md
```

## Test Coverage

### Controllers
- **UserSitesControllerTests**: Tests for the main controller actions including Index, Sites, and Forward methods

### Models
- **SiteThumbnailTests**: Tests for the SiteThumbnail model properties and behavior
- **SiteThumbnailsManagerTests**: Tests for the static SiteThumbnailsManager class including GetImageName method

### Services
- **ConfigurationServiceTests**: Tests for configuration reading and parsing
- **IPHelperTests**: Tests for IP validation and VPN access detection

### Helpers
- **CryptographyTests**: Tests for encryption functionality
- **StringExtensionsTests**: Tests for string manipulation utilities

## Framework Conversion

The project has been successfully converted from xUnit to NUnit with the following changes:

### Attribute Changes
- `[Fact]` → `[Test]`
- `[Theory]` → `[TestCase]`
- `[InlineData]` → `[TestCase]`
- Added `[TestFixture]` to all test classes

### Assertion Changes
- `Assert.Equal(expected, actual)` → `Assert.That(actual, Is.EqualTo(expected))`
- `Assert.Null(result)` → `Assert.That(result, Is.Null)`
- `Assert.NotNull(result)` → `Assert.That(result, Is.Not.Null)`
- `Assert.True(result)` → `Assert.That(result, Is.True)`
- `Assert.False(result)` → `Assert.That(result, Is.False)`
- `Assert.Empty(result)` → `Assert.That(result, Is.Empty)`
- `Assert.Contains(item, collection)` → `Assert.That(collection, Does.Contain(item))`
- `Assert.IsType<T>(result)` → `Assert.That(result, Is.InstanceOf<T>())`

## Dependencies

- **NUnit**: 4.3.2 - Main testing framework
- **NUnit3TestAdapter**: 5.0.0 - Test adapter for Visual Studio
- **Moq**: 4.20.72 - Mocking framework for unit tests
- **Microsoft.NET.Test.Sdk**: 17.12.0 - Test SDK
- **coverlet.collector**: 6.0.2 - Code coverage collection

## Running Tests

To run all tests:
```bash
dotnet test
```

To run tests with coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Test Results

- **Total Tests**: 58
- **Passed**: 58
- **Failed**: 0
- **Skipped**: 0

All tests are currently passing and provide comprehensive coverage of the main application components. 