namespace ArticlesManager.IntegrationTests.FeatureTests.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.ArticleImages.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class AddArticleImageCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_articleimage_to_db()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeArticleImageOne = new FakeArticleImageForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            .Generate();

        // Act
        var command = new AddArticleImage.AddArticleImageCommand(fakeArticleImageOne);
        var articleImageReturned = await SendAsync(command);
        var articleImageCreated = await ExecuteDbContextAsync(db => db.ArticleImages
            .FirstOrDefaultAsync(a => a.Id == articleImageReturned.Id));

        // Assert
        articleImageReturned.Should().BeEquivalentTo(fakeArticleImageOne, options =>
            options.ExcludingMissingMembers());
        articleImageCreated.Should().BeEquivalentTo(fakeArticleImageOne, options =>
            options.ExcludingMissingMembers());
    }
}