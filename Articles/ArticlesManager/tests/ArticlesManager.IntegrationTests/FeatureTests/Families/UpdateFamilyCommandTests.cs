namespace ArticlesManager.IntegrationTests.FeatureTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.Domain.Families.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Families.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_family_in_db()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        var updatedFamilyDto = new FakeFamilyForUpdateDto().Generate();
        await InsertAsync(fakeFamilyOne);

        var family = await ExecuteDbContextAsync(db => db.Families
            .FirstOrDefaultAsync(f => f.Id == fakeFamilyOne.Id));
        var id = family.Id;

        // Act
        var command = new UpdateFamily.UpdateFamilyCommand(id, updatedFamilyDto);
        await SendAsync(command);
        var updatedFamily = await ExecuteDbContextAsync(db => db.Families.FirstOrDefaultAsync(f => f.Id == id));

        // Assert
        updatedFamily.Should().BeEquivalentTo(updatedFamilyDto, options =>
            options.ExcludingMissingMembers());
    }
}