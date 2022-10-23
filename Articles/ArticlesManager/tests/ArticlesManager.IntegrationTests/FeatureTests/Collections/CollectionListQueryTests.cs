namespace ArticlesManager.IntegrationTests.FeatureTests.Collections;

using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Collections.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class CollectionListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_collection_list()
    {
        // Arrange
        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        var fakeCollectionTwo = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        var queryParameters = new CollectionParametersDto();

        await InsertAsync(fakeCollectionOne, fakeCollectionTwo);

        // Act
        var query = new GetCollectionList.CollectionListQuery(queryParameters);
        var collections = await SendAsync(query);

        // Assert
        collections.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}