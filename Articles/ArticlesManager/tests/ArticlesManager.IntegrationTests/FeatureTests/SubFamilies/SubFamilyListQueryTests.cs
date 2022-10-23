namespace ArticlesManager.IntegrationTests.FeatureTests.SubFamilies;

using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.SubFamilies.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class SubFamilyListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_subfamily_list()
    {
        // Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        var fakeSubFamilyTwo = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        var queryParameters = new SubFamilyParametersDto();

        await InsertAsync(fakeSubFamilyOne, fakeSubFamilyTwo);

        // Act
        var query = new GetSubFamilyList.SubFamilyListQuery(queryParameters);
        var subFamilies = await SendAsync(query);

        // Assert
        subFamilies.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}