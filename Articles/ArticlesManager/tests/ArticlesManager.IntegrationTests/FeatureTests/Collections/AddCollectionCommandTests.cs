namespace ArticlesManager.IntegrationTests.FeatureTests.Collections;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.Collections.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddCollectionCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_collection_to_db()
    {
        // Arrange
        var fakeCollectionOne = new FakeCollectionForCreationDto().Generate();

        // Act
        var command = new AddCollection.AddCollectionCommand(fakeCollectionOne);
        var collectionReturned = await SendAsync(command);
        var collectionCreated = await ExecuteDbContextAsync(db => db.Collections
            .FirstOrDefaultAsync(c => c.Id == collectionReturned.Id));

        // Assert
        collectionReturned.Should().BeEquivalentTo(fakeCollectionOne, options =>
            options.ExcludingMissingMembers());
        collectionCreated.Should().BeEquivalentTo(fakeCollectionOne, options =>
            options.ExcludingMissingMembers());
    }
}