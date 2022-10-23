namespace ArticlesManager.FunctionalTests.FunctionalTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateCollectionTests : TestBase
{
    [Test]
    public async Task create_collection_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeCollection = new FakeCollectionForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Collections.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeCollection);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}