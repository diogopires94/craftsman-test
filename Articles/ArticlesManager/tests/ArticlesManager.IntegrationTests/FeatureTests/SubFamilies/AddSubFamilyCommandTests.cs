namespace ArticlesManager.IntegrationTests.FeatureTests.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.SubFamilies.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddSubFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_subfamily_to_db()
    {
        // Arrange
        var fakeSubFamilyOne = new FakeSubFamilyForCreationDto().Generate();

        // Act
        var command = new AddSubFamily.AddSubFamilyCommand(fakeSubFamilyOne);
        var subFamilyReturned = await SendAsync(command);
        var subFamilyCreated = await ExecuteDbContextAsync(db => db.SubFamilies
            .FirstOrDefaultAsync(s => s.Id == subFamilyReturned.Id));

        // Assert
        subFamilyReturned.Should().BeEquivalentTo(fakeSubFamilyOne, options =>
            options.ExcludingMissingMembers());
        subFamilyCreated.Should().BeEquivalentTo(fakeSubFamilyOne, options =>
            options.ExcludingMissingMembers());
    }
}