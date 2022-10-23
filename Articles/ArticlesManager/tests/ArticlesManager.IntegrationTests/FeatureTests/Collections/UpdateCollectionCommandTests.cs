namespace ArticlesManager.IntegrationTests.FeatureTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.Domain.Collections.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Collections.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateCollectionCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_collection_in_db()
    {
        // Arrange
        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        var updatedCollectionDto = new FakeCollectionForUpdateDto().Generate();
        await InsertAsync(fakeCollectionOne);

        var collection = await ExecuteDbContextAsync(db => db.Collections
            .FirstOrDefaultAsync(c => c.Id == fakeCollectionOne.Id));
        var id = collection.Id;

        // Act
        var command = new UpdateCollection.UpdateCollectionCommand(id, updatedCollectionDto);
        await SendAsync(command);
        var updatedCollection = await ExecuteDbContextAsync(db => db.Collections.FirstOrDefaultAsync(c => c.Id == id));

        // Assert
        updatedCollection.Should().BeEquivalentTo(updatedCollectionDto, options =>
            options.ExcludingMissingMembers());
    }
}