namespace ArticlesManager.FunctionalTests.FunctionalTests.SubFamilies;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteSubFamilyTests : TestBase
{
    [Test]
    public async Task delete_subfamily_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeSubFamily = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamily);

        // Act
        var route = ApiRoutes.SubFamilies.Delete.Replace(ApiRoutes.SubFamilies.Id, fakeSubFamily.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}