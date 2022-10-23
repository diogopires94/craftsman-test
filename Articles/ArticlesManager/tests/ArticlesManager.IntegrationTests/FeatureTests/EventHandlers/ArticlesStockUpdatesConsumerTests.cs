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

public class ArticlesStockUpdatesConsumerTests : TestBase
{
    [Test]
    public async Task can_consume_IArticleStockUpdate_message()
    {
        // Arrange
        var message = new Mock<IIArticleStockUpdate>();

        // Act
        await PublishMessage<IIArticleStockUpdate>(message);

        // Assert
        (await IsConsumed<IIArticleStockUpdate>()).Should().Be(true);
        (await IsConsumed<IIArticleStockUpdate, ArticlesStockUpdatesConsumer>()).Should().Be(true);
    }
}