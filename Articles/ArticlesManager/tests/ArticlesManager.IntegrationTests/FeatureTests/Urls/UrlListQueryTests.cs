namespace ArticlesManager.IntegrationTests.FeatureTests.Urls;

using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.Url;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Urls.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UrlListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_url_list()
    {
        // Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        var queryParameters = new UrlParametersDto();

        await InsertAsync(fakeUrlOne, fakeUrlTwo);

        // Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var urls = await SendAsync(query);

        // Assert
        urls.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}