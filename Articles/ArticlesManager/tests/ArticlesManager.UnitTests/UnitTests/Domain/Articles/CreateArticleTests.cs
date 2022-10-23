namespace ArticlesManager.UnitTests.UnitTests.Domain.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Articles.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateArticleTests
{
    private readonly Faker _faker;

    public CreateArticleTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_article()
    {
        // Arrange + Act
        var fakeArticle = FakeArticle.Generate();

        // Assert
        fakeArticle.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeArticle = FakeArticle.Generate();

        // Assert
        fakeArticle.DomainEvents.Count.Should().Be(1);
        fakeArticle.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ArticleCreated));
    }
}