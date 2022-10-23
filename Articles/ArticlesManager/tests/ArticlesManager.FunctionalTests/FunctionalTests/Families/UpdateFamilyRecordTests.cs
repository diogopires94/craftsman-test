namespace ArticlesManager.FunctionalTests.FunctionalTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateFamilyRecordTests : TestBase
{
    [Test]
    public async Task put_family_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFamily = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        var updatedFamilyDto = new FakeFamilyForUpdateDto { }.Generate();
        await InsertAsync(fakeFamily);

        // Act
        var route = ApiRoutes.Families.Put.Replace(ApiRoutes.Families.Id, fakeFamily.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedFamilyDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}