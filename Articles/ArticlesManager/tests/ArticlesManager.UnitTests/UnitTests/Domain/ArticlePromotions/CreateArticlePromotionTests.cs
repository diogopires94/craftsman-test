namespace ArticlesManager.UnitTests.UnitTests.Domain.ArticlePromotions;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Domain.ArticlePromotions.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateArticlePromotionTests
{
    private readonly Faker _faker;

    public CreateArticlePromotionTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_articlePromotion()
    {
        // Arrange + Act
        var fakeArticlePromotion = FakeArticlePromotion.Generate();

        // Assert
        fakeArticlePromotion.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeArticlePromotion = FakeArticlePromotion.Generate();

        // Assert
        fakeArticlePromotion.DomainEvents.Count.Should().Be(1);
        fakeArticlePromotion.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ArticlePromotionCreated));
    }
}