namespace ArticlesManager.FunctionalTests.FunctionalTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetFamilyTests : TestBase
{
    [Test]
    public async Task get_family_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeFamily = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamily);

        // Act
        var route = ApiRoutes.Families.GetRecord.Replace(ApiRoutes.Families.Id, fakeFamily.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}