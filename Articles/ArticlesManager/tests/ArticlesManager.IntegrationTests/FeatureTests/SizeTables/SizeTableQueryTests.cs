namespace ArticlesManager.IntegrationTests.FeatureTests.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.Domain.SizeTables.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;

public class SizeTableQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_sizetable_with_accurate_props()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);

        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(s => s.SubFamilyId, _ => fakeSubFamilyOne.Id)
            .Generate());
        await InsertAsync(fakeSizeTableOne);

        // Act
        var query = new GetSizeTable.SizeTableQuery(fakeSizeTableOne.Id);
        var sizeTable = await SendAsync(query);

        // Assert
        sizeTable.Should().BeEquivalentTo(fakeSizeTableOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_sizetable_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetSizeTable.SizeTableQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}