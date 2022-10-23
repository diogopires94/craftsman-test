namespace ArticlesManager.IntegrationTests.FeatureTests.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.Domain.ArticleImages.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.ArticleImages.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class UpdateArticleImageCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_articleimage_in_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeArticleImageOne = FakeArticleImage.Generate(new FakeArticleImageForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        var updatedArticleImageDto = new FakeArticleImageForUpdateDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            .Generate();
        await InsertAsync(fakeArticleImageOne);

        var articleImage = await ExecuteDbContextAsync(db => db.ArticleImages
            .FirstOrDefaultAsync(a => a.Id == fakeArticleImageOne.Id));
        var id = articleImage.Id;

        // Act
        var command = new UpdateArticleImage.UpdateArticleImageCommand(id, updatedArticleImageDto);
        await SendAsync(command);
        var updatedArticleImage = await ExecuteDbContextAsync(db => db.ArticleImages.FirstOrDefaultAsync(a => a.Id == id));

        // Assert
        updatedArticleImage.Should().BeEquivalentTo(updatedArticleImageDto, options =>
            options.ExcludingMissingMembers());
    }
}