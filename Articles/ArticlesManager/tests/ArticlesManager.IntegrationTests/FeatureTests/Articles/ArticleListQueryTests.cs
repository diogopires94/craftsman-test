namespace ArticlesManager.IntegrationTests.FeatureTests.Articles;

using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.SharedTestHelpers.Fakes.Article;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Articles.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class ArticleListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_article_list()
    {
        // Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
    var fakeBrandTwo = FakeBrand.Generate(new FakeBrandForCreationDto().Generate());
    await InsertAsync(fakeBrandOne, fakeBrandTwo);

        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
    var fakeFamilyTwo = FakeFamily.Generate(new FakeFamilyForCreationDto().Generate());
    await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
    var fakeSubFamilyTwo = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto().Generate());
    await InsertAsync(fakeSubFamilyOne, fakeSubFamilyTwo);

        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
    var fakeCollectionTwo = FakeCollection.Generate(new FakeCollectionForCreationDto().Generate());
    await InsertAsync(fakeCollectionOne, fakeCollectionTwo);

        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(a => a.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(a => a.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(a => a.CollectionId, _ => fakeCollectionOne.Id)
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.BrandId, _ => fakeBrandTwo.Id)
            
            .RuleFor(a => a.FamilyId, _ => fakeFamilyTwo.Id)
            
            .RuleFor(a => a.SubFamilyId, _ => fakeSubFamilyTwo.Id)
            
            .RuleFor(a => a.CollectionId, _ => fakeCollectionTwo.Id)
            .Generate());
        var queryParameters = new ArticleParametersDto();

        await InsertAsync(fakeArticleOne, fakeArticleTwo);

        // Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var articles = await SendAsync(query);

        // Assert
        articles.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}