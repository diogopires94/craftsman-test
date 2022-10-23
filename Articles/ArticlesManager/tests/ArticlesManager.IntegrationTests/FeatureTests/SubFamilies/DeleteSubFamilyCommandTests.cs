namespace ArticlesManager.IntegrationTests.FeatureTests.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.Domain.SubFamilies.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteSubFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_delete_subfamily_from_db()
    {
        // Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);
        var subFamily = await ExecuteDbContextAsync(db => db.SubFamilies
            .FirstOrDefaultAsync(s => s.Id == fakeSubFamilyOne.Id));

        // Act
        var command = new DeleteSubFamily.DeleteSubFamilyCommand(subFamily.Id);
        await SendAsync(command);
        var subFamilyResponse = await ExecuteDbContextAsync(db => db.SubFamilies.CountAsync(s => s.Id == subFamily.Id));

        // Assert
        subFamilyResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_subfamily_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteSubFamily.DeleteSubFamilyCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_subfamily_from_db()
    {
        // Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);
        var subFamily = await ExecuteDbContextAsync(db => db.SubFamilies
            .FirstOrDefaultAsync(s => s.Id == fakeSubFamilyOne.Id));

        // Act
        var command = new DeleteSubFamily.DeleteSubFamilyCommand(subFamily.Id);
        await SendAsync(command);
        var deletedSubFamily = await ExecuteDbContextAsync(db => db.SubFamilies
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == subFamily.Id));

        // Assert
        deletedSubFamily?.IsDeleted.Should().BeTrue();
    }
}