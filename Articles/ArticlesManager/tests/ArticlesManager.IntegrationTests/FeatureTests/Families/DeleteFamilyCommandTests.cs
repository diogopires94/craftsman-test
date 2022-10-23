namespace ArticlesManager.IntegrationTests.FeatureTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.Domain.Families.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_delete_family_from_db()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);
        var family = await ExecuteDbContextAsync(db => db.Families
            .FirstOrDefaultAsync(f => f.Id == fakeFamilyOne.Id));

        // Act
        var command = new DeleteFamily.DeleteFamilyCommand(family.Id);
        await SendAsync(command);
        var familyResponse = await ExecuteDbContextAsync(db => db.Families.CountAsync(f => f.Id == family.Id));

        // Assert
        familyResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_family_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteFamily.DeleteFamilyCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_family_from_db()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);
        var family = await ExecuteDbContextAsync(db => db.Families
            .FirstOrDefaultAsync(f => f.Id == fakeFamilyOne.Id));

        // Act
        var command = new DeleteFamily.DeleteFamilyCommand(family.Id);
        await SendAsync(command);
        var deletedFamily = await ExecuteDbContextAsync(db => db.Families
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == family.Id));

        // Assert
        deletedFamily?.IsDeleted.Should().BeTrue();
    }
}