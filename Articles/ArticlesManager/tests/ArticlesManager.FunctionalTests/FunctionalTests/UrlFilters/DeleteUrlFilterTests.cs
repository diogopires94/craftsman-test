namespace ArticlesManager.FunctionalTests.FunctionalTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteUrlFilterTests : TestBase
{
    [Test]
    public async Task delete_urlfilter_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUrlFilter = FakeUrlFilter.Generate(new FakeUrlFilterForCreationDto().Generate());
        await InsertAsync(fakeUrlFilter);

        // Act
        var route = ApiRoutes.UrlFilters.Delete.Replace(ApiRoutes.UrlFilters.Id, fakeUrlFilter.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}