namespace ArticlesManager.IntegrationTests.FeatureTests.EventHandlers;

using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using SharedKernel.Messages;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ArticlesManager.Domain;
using static TestFixture;

public class ArticlesSyncConsumerTests : TestBase
{
    [Test]
    public async Task can_consume_IArticles_message()
    {
        // Arrange
        var message = new Mock<IIArticles>();

        // Act
        await PublishMessage<IIArticles>(message);

        // Assert
        (await IsConsumed<IIArticles>()).Should().Be(true);
        (await IsConsumed<IIArticles, ArticlesSyncConsumer>()).Should().Be(true);
    }
}