namespace ArticlesManager.IntegrationTests.FeatureTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.Families.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_family_to_db()
    {
        // Arrange
        var fakeFamilyOne = new FakeFamilyForCreationDto().Generate();

        // Act
        var command = new AddFamily.AddFamilyCommand(fakeFamilyOne);
        var familyReturned = await SendAsync(command);
        var familyCreated = await ExecuteDbContextAsync(db => db.Families
            .FirstOrDefaultAsync(f => f.Id == familyReturned.Id));

        // Assert
        familyReturned.Should().BeEquivalentTo(fakeFamilyOne, options =>
            options.ExcludingMissingMembers());
        familyCreated.Should().BeEquivalentTo(fakeFamilyOne, options =>
            options.ExcludingMissingMembers());
    }
}