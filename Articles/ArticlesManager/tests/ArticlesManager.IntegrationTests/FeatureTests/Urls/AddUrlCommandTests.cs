namespace ArticlesManager.IntegrationTests.FeatureTests.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.Urls.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddUrlCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_url_to_db()
    {
        // Arrange
        var fakeUrlOne = new FakeUrlForCreationDto().Generate();

        // Act
        var command = new AddUrl.AddUrlCommand(fakeUrlOne);
        var urlReturned = await SendAsync(command);
        var urlCreated = await ExecuteDbContextAsync(db => db.Urls
            .FirstOrDefaultAsync(u => u.Id == urlReturned.Id));

        // Assert
        urlReturned.Should().BeEquivalentTo(fakeUrlOne, options =>
            options.ExcludingMissingMembers());
        urlCreated.Should().BeEquivalentTo(fakeUrlOne, options =>
            options.ExcludingMissingMembers());
    }
}