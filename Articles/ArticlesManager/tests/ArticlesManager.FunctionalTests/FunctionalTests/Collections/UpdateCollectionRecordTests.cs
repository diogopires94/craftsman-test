namespace ArticlesManager.FunctionalTests.FunctionalTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateCollectionRecordTests : TestBase
{
    [Test]
    public async Task put_collection_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeCollection = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        var updatedCollectionDto = new FakeCollectionForUpdateDto { }.Generate();
        await InsertAsync(fakeCollection);

        // Act
        var route = ApiRoutes.Collections.Put.Replace(ApiRoutes.Collections.Id, fakeCollection.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedCollectionDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}