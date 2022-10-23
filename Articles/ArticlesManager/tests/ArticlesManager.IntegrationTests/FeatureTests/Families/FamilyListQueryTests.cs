namespace ArticlesManager.IntegrationTests.FeatureTests.Families;

using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Families.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class FamilyListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_family_list()
    {
        // Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        var fakeFamilyTwo = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        var queryParameters = new FamilyParametersDto();

        await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        // Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var families = await SendAsync(query);

        // Assert
        families.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}