namespace ArticlesManager.IntegrationTests.FeatureTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.Domain.Collections.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class CollectionQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_collection_with_accurate_props()
    {
        // Arrange
        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollectionOne);

        // Act
        var query = new GetCollection.CollectionQuery(fakeCollectionOne.Id);
        var collection = await SendAsync(query);

        // Assert
        collection.Should().BeEquivalentTo(fakeCollectionOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_collection_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetCollection.CollectionQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}