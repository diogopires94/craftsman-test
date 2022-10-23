namespace ArticlesManager.FunctionalTests.FunctionalTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetCollectionTests : TestBase
{
    [Test]
    public async Task get_collection_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeCollection = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollection);

        // Act
        var route = ApiRoutes.Collections.GetRecord.Replace(ApiRoutes.Collections.Id, fakeCollection.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}