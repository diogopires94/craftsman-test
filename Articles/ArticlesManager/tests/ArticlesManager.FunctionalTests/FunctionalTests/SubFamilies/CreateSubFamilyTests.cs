namespace ArticlesManager.FunctionalTests.FunctionalTests.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateSubFamilyTests : TestBase
{
    [Test]
    public async Task create_subfamily_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeSubFamily = new FakeSubFamilyForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.SubFamilies.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeSubFamily);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}