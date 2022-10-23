namespace ArticlesManager.IntegrationTests.FeatureTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.Domain.Articles.Dtos;
using SharedKernel.Exceptions;
using ArticlesManager.Domain.Articles.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class UpdateArticleCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_article_in_db()
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
        var updatedArticleDto = new FakeArticleForUpdateDto()
            .RuleFor(a => a.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(a => a.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(a => a.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(a => a.CollectionId, _ => fakeCollectionOne.Id)
            .Generate();
        await InsertAsync(fakeArticleOne);

        var article = await ExecuteDbContextAsync(db => db.Articles
            .FirstOrDefaultAsync(a => a.Id == fakeArticleOne.Id));
        var id = article.Id;

        // Act
        var command = new UpdateArticle.UpdateArticleCommand(id, updatedArticleDto);
        await SendAsync(command);
        var updatedArticle = await ExecuteDbContextAsync(db => db.Articles.FirstOrDefaultAsync(a => a.Id == id));

        // Assert
        updatedArticle.Should().BeEquivalentTo(updatedArticleDto, options =>
            options.ExcludingMissingMembers());
    }
}