namespace ArticlesManager.IntegrationTests.FeatureTests.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.Domain.ArticleImages.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class DeleteArticleImageCommandTests : TestBase
{
    [Test]
    public async Task can_delete_articleimage_from_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeArticleImageOne = FakeArticleImage.Generate(new FakeArticleImageForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeArticleImageOne);
        var articleImage = await ExecuteDbContextAsync(db => db.ArticleImages
            .FirstOrDefaultAsync(a => a.Id == fakeArticleImageOne.Id));

        // Act
        var command = new DeleteArticleImage.DeleteArticleImageCommand(articleImage.Id);
        await SendAsync(command);
        var articleImageResponse = await ExecuteDbContextAsync(db => db.ArticleImages.CountAsync(a => a.Id == articleImage.Id));

        // Assert
        articleImageResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_articleimage_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteArticleImage.DeleteArticleImageCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_articleimage_from_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeArticleImageOne = FakeArticleImage.Generate(new FakeArticleImageForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeArticleImageOne);
        var articleImage = await ExecuteDbContextAsync(db => db.ArticleImages
            .FirstOrDefaultAsync(a => a.Id == fakeArticleImageOne.Id));

        // Act
        var command = new DeleteArticleImage.DeleteArticleImageCommand(articleImage.Id);
        await SendAsync(command);
        var deletedArticleImage = await ExecuteDbContextAsync(db => db.ArticleImages
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == articleImage.Id));

        // Assert
        deletedArticleImage?.IsDeleted.Should().BeTrue();
    }
}