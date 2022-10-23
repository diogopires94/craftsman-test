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

public class ArticleImageQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_articleimage_with_accurate_props()
    {
        // Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto().Generate());
        await InsertAsync(fakeArticleOne);

        var fakeArticleImageOne = FakeArticleImage.Generate(new FakeArticleImageForCreationDto()
            .RuleFor(a => a.ArticleId, _ => fakeArticleOne.Id)
            .Generate());
        await InsertAsync(fakeArticleImageOne);

        // Act
        var query = new GetArticleImage.ArticleImageQuery(fakeArticleImageOne.Id);
        var articleImage = await SendAsync(query);

        // Assert
        articleImage.Should().BeEquivalentTo(fakeArticleImageOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_articleimage_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetArticleImage.ArticleImageQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}