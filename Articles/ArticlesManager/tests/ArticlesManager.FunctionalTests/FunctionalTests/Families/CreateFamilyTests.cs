namespace ArticlesManager.FunctionalTests.FunctionalTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateFamilyTests : TestBase
{
    [Test]
    public async Task create_family_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeFamily = new FakeFamilyForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Families.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeFamily);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}