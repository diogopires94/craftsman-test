namespace ArticlesManager.IntegrationTests.FeatureTests.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.Domain.Urls.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Urls.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateUrlCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_url_in_db()
    {
        // Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        var updatedUrlDto = new FakeUrlForUpdateDto().Generate();
        await InsertAsync(fakeUrlOne);

        var url = await ExecuteDbContextAsync(db => db.Urls
            .FirstOrDefaultAsync(u => u.Id == fakeUrlOne.Id));
        var id = url.Id;

        // Act
        var command = new UpdateUrl.UpdateUrlCommand(id, updatedUrlDto);
        await SendAsync(command);
        var updatedUrl = await ExecuteDbContextAsync(db => db.Urls.FirstOrDefaultAsync(u => u.Id == id));

        // Assert
        updatedUrl.Should().BeEquivalentTo(updatedUrlDto, options =>
            options.ExcludingMissingMembers());
    }
}