namespace ArticlesManager.IntegrationTests.FeatureTests.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ArticlesManager.Domain.Articles.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.SharedTestHelpers.Fakes.Collection;

public class AddArticleCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_article_to_db()
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

        var fakeArticleOne = new FakeArticleForCreationDto()
            .RuleFor(a => a.BrandId, _ => fakeBrandOne.Id)
            
            .RuleFor(a => a.FamilyId, _ => fakeFamilyOne.Id)
            
            .RuleFor(a => a.SubFamilyId, _ => fakeSubFamilyOne.Id)
            
            .RuleFor(a => a.CollectionId, _ => fakeCollectionOne.Id)
            .Generate();

        // Act
        var command = new AddArticle.AddArticleCommand(fakeArticleOne);
        var articleReturned = await SendAsync(command);
        var articleCreated = await ExecuteDbContextAsync(db => db.Articles
            .FirstOrDefaultAsync(a => a.Id == articleReturned.Id));

        // Assert
        articleReturned.Should().BeEquivalentTo(fakeArticleOne, options =>
            options.ExcludingMissingMembers());
        articleCreated.Should().BeEquivalentTo(fakeArticleOne, options =>
            options.ExcludingMissingMembers());
    }
}