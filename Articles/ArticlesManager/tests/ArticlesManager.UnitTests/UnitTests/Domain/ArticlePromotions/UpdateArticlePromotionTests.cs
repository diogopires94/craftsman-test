namespace ArticlesManager.UnitTests.UnitTests.Domain.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Domain.ArticlePromotions.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateArticlePromotionTests
{
    private readonly Faker _faker;

    public UpdateArticlePromotionTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_articlePromotion()
    {
        // Arrange
        var fakeArticlePromotion = FakeArticlePromotion.Generate();
        var updatedArticlePromotion = new FakeArticlePromotionForUpdateDto().Generate();
        
        // Act
        fakeArticlePromotion.Update(updatedArticlePromotion);

        // Assert
        fakeArticlePromotion.Should().BeEquivalentTo(updatedArticlePromotion, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeArticlePromotion = FakeArticlePromotion.Generate();
        var updatedArticlePromotion = new FakeArticlePromotionForUpdateDto().Generate();
        fakeArticlePromotion.DomainEvents.Clear();
        
        // Act
        fakeArticlePromotion.Update(updatedArticlePromotion);

        // Assert
        fakeArticlePromotion.DomainEvents.Count.Should().Be(1);
        fakeArticlePromotion.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ArticlePromotionUpdated));
    }
}