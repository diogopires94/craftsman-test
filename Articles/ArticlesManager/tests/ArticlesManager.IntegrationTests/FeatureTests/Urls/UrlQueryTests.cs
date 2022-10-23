namespace ArticlesManager.IntegrationTests.FeatureTests.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.Domain.Urls.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class UrlQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_url_with_accurate_props()
    {
        // Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        await InsertAsync(fakeUrlOne);

        // Act
        var query = new GetUrl.UrlQuery(fakeUrlOne.Id);
        var url = await SendAsync(query);

        // Assert
        url.Should().BeEquivalentTo(fakeUrlOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_url_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetUrl.UrlQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}