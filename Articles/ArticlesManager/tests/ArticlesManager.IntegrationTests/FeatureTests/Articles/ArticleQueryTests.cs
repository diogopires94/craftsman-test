namespace ArticlesManager.IntegrationTests.FeatureTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.Domain.Articles.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class ArticleQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_article_with_accurate_props()
    {
        // Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
        await InsertAsync(fakeBrandOne);

        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
        await InsertAsync(fakeFamilyOne);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
        await InsertAsync(fakeSubFamilyOne);

        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
        await InsertAsync(fakeCollectionOne);

        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(a => a.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(a => a.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(a => a.CollectionId, _ => fakeCollectionOne.Id)
            .Generate());
        await InsertAsync(fakeArticleOne);

        // Act
        var query = new GetArticle.ArticleQuery(fakeArticleOne.Id);
        var article = await SendAsync(query);

        // Assert
        article.Should().BeEquivalentTo(fakeArticleOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_article_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetArticle.ArticleQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}