namespace ArticlesManager.IntegrationTests.FeatureTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.Domain.Collections.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteCollectionCommandTests : TestBase
{
    [Test]
    public async Task can_delete_collection_from_db()
    {
        // Arrange
        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollectionOne);
        var collection = await ExecuteDbContextAsync(db => db.Collections
            .FirstOrDefaultAsync(c => c.Id == fakeCollectionOne.Id));

        // Act
        var command = new DeleteCollection.DeleteCollectionCommand(collection.Id);
        await SendAsync(command);
        var collectionResponse = await ExecuteDbContextAsync(db => db.Collections.CountAsync(c => c.Id == collection.Id));

        // Assert
        collectionResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_collection_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteCollection.DeleteCollectionCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_collection_from_db()
    {
        // Arrange
        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollectionOne);
        var collection = await ExecuteDbContextAsync(db => db.Collections
            .FirstOrDefaultAsync(c => c.Id == fakeCollectionOne.Id));

        // Act
        var command = new DeleteCollection.DeleteCollectionCommand(collection.Id);
        await SendAsync(command);
        var deletedCollection = await ExecuteDbContextAsync(db => db.Collections
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == collection.Id));

        // Assert
        deletedCollection?.IsDeleted.Should().BeTrue();
    }
}