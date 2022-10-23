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

public class DeleteArticleCommandTests : TestBase
{
    [Test]
    public async Task can_delete_article_from_db()
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
        var article = await ExecuteDbContextAsync(db => db.Articles
            .FirstOrDefaultAsync(a => a.Id == fakeArticleOne.Id));

        // Act
        var command = new DeleteArticle.DeleteArticleCommand(article.Id);
        await SendAsync(command);
        var articleResponse = await ExecuteDbContextAsync(db => db.Articles.CountAsync(a => a.Id == article.Id));

        // Assert
        articleResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_article_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteArticle.DeleteArticleCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_article_from_db()
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
        var article = await ExecuteDbContextAsync(db => db.Articles
            .FirstOrDefaultAsync(a => a.Id == fakeArticleOne.Id));

        // Act
        var command = new DeleteArticle.DeleteArticleCommand(article.Id);
        await SendAsync(command);
        var deletedArticle = await ExecuteDbContextAsync(db => db.Articles
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == article.Id));

        // Assert
        deletedArticle?.IsDeleted.Should().BeTrue();
    }
}