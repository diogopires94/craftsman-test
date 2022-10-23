namespace ArticlesManager.UnitTests.UnitTests.Domain.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.Domain.ArticleImages;
using ArticlesManager.Domain.ArticleImages.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateArticleImageTests
{
    private readonly Faker _faker;

    public CreateArticleImageTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_articleImage()
    {
        // Arrange + Act
        var fakeArticleImage = FakeArticleImage.Generate();

        // Assert
        fakeArticleImage.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeArticleImage = FakeArticleImage.Generate();

        // Assert
        fakeArticleImage.DomainEvents.Count.Should().Be(1);
        fakeArticleImage.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ArticleImageCreated));
    }
}