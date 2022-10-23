namespace ArticlesManager.IntegrationTests.FeatureTests.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.Domain.SubFamilies.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class SubFamilyQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_subfamily_with_accurate_props()
    {
        // Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);

        // Act
        var query = new GetSubFamily.SubFamilyQuery(fakeSubFamilyOne.Id);
        var subFamily = await SendAsync(query);

        // Assert
        subFamily.Should().BeEquivalentTo(fakeSubFamilyOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_subfamily_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetSubFamily.SubFamilyQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}