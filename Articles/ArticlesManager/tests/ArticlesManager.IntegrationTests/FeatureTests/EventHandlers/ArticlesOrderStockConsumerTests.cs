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

public class ArticlesOrderStockConsumerTests : TestBase
{
    [Test]
    public async Task can_consume_IOrderStock_message()
    {
        // Arrange
        var message = new Mock<IIOrderStock>();

        // Act
        await PublishMessage<IIOrderStock>(message);

        // Assert
        (await IsConsumed<IIOrderStock>()).Should().Be(true);
        (await IsConsumed<IIOrderStock, ArticlesOrderStockConsumer>()).Should().Be(true);
    }
}