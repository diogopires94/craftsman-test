namespace ArticlesManager.UnitTests.UnitTests.Domain.ArticleImages;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.Domain.ArticleImages;
using ArticlesManager.Domain.ArticleImages.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateArticleImageTests
{
    private readonly Faker _faker;

    public UpdateArticleImageTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_articleImage()
    {
        // Arrange
        var fakeArticleImage = FakeArticleImage.Generate();
        var updatedArticleImage = new FakeArticleImageForUpdateDto().Generate();
        
        // Act
        fakeArticleImage.Update(updatedArticleImage);

        // Assert
        fakeArticleImage.Should().BeEquivalentTo(updatedArticleImage, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeArticleImage = FakeArticleImage.Generate();
        var updatedArticleImage = new FakeArticleImageForUpdateDto().Generate();
        fakeArticleImage.DomainEvents.Clear();
        
        // Act
        fakeArticleImage.Update(updatedArticleImage);

        // Assert
        fakeArticleImage.DomainEvents.Count.Should().Be(1);
        fakeArticleImage.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ArticleImageUpdated));
    }
}