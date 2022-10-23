namespace ArticlesManager.FunctionalTests.FunctionalTests.Families;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteFamilyTests : TestBase
{
    [Test]
    public async Task delete_family_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFamily = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamily);

        // Act
        var route = ApiRoutes.Families.Delete.Replace(ApiRoutes.Families.Id, fakeFamily.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}