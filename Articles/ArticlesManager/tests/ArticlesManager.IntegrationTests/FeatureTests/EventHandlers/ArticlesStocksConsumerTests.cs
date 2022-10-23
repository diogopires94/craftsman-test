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

public class ArticlesStocksConsumerTests : TestBase
{
    [Test]
    public async Task can_consume_IStocks_message()
    {
        // Arrange
        var message = new Mock<IIStocks>();

        // Act
        await PublishMessage<IIStocks>(message);

        // Assert
        (await IsConsumed<IIStocks>()).Should().Be(true);
        (await IsConsumed<IIStocks, ArticlesStocksConsumer>()).Should().Be(true);
    }
}