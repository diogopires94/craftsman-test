namespace ArticlesManager.FunctionalTests.FunctionalTests.UrlFilters;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateUrlFilterRecordTests : TestBase
{
    [Test]
    public async Task put_urlfilter_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUrlFilter = FakeUrlFilter.Generate(new FakeUrlFilterForCreationDto().Generate());
        var updatedUrlFilterDto = new FakeUrlFilterForUpdateDto { }.Generate();
        await InsertAsync(fakeUrlFilter);

        // Act
        var route = ApiRoutes.UrlFilters.Put.Replace(ApiRoutes.UrlFilters.Id, fakeUrlFilter.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedUrlFilterDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}