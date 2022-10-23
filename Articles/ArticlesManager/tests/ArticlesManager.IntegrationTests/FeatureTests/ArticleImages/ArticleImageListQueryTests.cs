namespace ArticlesManager.IntegrationTests.FeatureTests.ArticleImages;

using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.ArticleImages.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Article;

public class ArticleImageListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_articleimage_list()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
    await InsertAsync(fakeArticleOne, fakeArticleTwo);

        var fakeArticleImageOne = FakeArticleImage.Generate(new FakeArticleImageForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        var fakeArticleImageTwo = FakeArticleImage.Generate(new FakeArticleImageForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleTwo.Id)
            .Generate());
        var queryParameters = new ArticleImageParametersDto();

        await InsertAsync(fakeArticleImageOne, fakeArticleImageTwo);

        // Act
        var query = new GetArticleImageList.ArticleImageListQuery(queryParameters);
        var articleImages = await SendAsync(query);

        // Assert
        articleImages.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}