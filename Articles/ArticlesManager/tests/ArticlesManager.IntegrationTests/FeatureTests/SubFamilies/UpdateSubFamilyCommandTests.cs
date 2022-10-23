namespace ArticlesManager.IntegrationTests.FeatureTests.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.Domain.SubFamilies.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.SubFamilies.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateSubFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_subfamily_in_db()
    {
        // Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        var updatedSubFamilyDto = new FakeSubFamilyForUpdateDto().Generate();
        await InsertAsync(fakeSubFamilyOne);

        var subFamily = await ExecuteDbContextAsync(db => db.SubFamilies
            .FirstOrDefaultAsync(s => s.Id == fakeSubFamilyOne.Id));
        var id = subFamily.Id;

        // Act
        var command = new UpdateSubFamily.UpdateSubFamilyCommand(id, updatedSubFamilyDto);
        await SendAsync(command);
        var updatedSubFamily = await ExecuteDbContextAsync(db => db.SubFamilies.FirstOrDefaultAsync(s => s.Id == id));

        // Assert
        updatedSubFamily.Should().BeEquivalentTo(updatedSubFamilyDto, options =>
            options.ExcludingMissingMembers());
    }
}