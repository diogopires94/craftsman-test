namespace ArticlesManager.IntegrationTests.FeatureTests.Urls;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.Domain.Urls.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteUrlCommandTests : TestBase
{
    [Test]
    public async Task can_delete_url_from_db()
    {
        // Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        await InsertAsync(fakeUrlOne);
        var url = await ExecuteDbContextAsync(db => db.Urls
            .FirstOrDefaultAsync(u => u.Id == fakeUrlOne.Id));

        // Act
        var command = new DeleteUrl.DeleteUrlCommand(url.Id);
        await SendAsync(command);
        var urlResponse = await ExecuteDbContextAsync(db => db.Urls.CountAsync(u => u.Id == url.Id));

        // Assert
        urlResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_url_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteUrl.DeleteUrlCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_url_from_db()
    {
        // Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto().Generate());
        await InsertAsync(fakeUrlOne);
        var url = await ExecuteDbContextAsync(db => db.Urls
            .FirstOrDefaultAsync(u => u.Id == fakeUrlOne.Id));

        // Act
        var command = new DeleteUrl.DeleteUrlCommand(url.Id);
        await SendAsync(command);
        var deletedUrl = await ExecuteDbContextAsync(db => db.Urls
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == url.Id));

        // Assert
        deletedUrl?.IsDeleted.Should().BeTrue();
    }
}