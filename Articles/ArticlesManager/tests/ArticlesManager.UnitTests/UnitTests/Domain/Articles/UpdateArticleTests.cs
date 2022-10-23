namespace ArticlesManager.UnitTests.UnitTests.Domain.Articles;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Articles.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateArticleTests
{
    private readonly Faker _faker;

    public UpdateArticleTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_article()
    {
        // Arrange
        var fakeArticle = FakeArticle.Generate();
        var updatedArticle = new FakeArticleForUpdateDto().Generate();
        
        // Act
        fakeArticle.Update(updatedArticle);

        // Assert
        fakeArticle.Should().BeEquivalentTo(updatedArticle, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeArticle = FakeArticle.Generate();
        var updatedArticle = new FakeArticleForUpdateDto().Generate();
        fakeArticle.DomainEvents.Clear();
        
        // Act
        fakeArticle.Update(updatedArticle);

        // Assert
        fakeArticle.DomainEvents.Count.Should().Be(1);
        fakeArticle.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ArticleUpdated));
    }
}