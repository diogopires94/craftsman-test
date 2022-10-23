namespace ArticlesManager.IntegrationTests.FeatureTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.Domain.Families.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class FamilyQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_family_with_accurate_props()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);

        // Act
        var query = new GetFamily.FamilyQuery(fakeFamilyOne.Id);
        var family = await SendAsync(query);

        // Assert
        family.Should().BeEquivalentTo(fakeFamilyOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_family_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetFamily.FamilyQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}