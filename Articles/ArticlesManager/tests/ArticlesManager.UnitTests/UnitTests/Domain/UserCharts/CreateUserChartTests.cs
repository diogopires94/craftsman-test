namespace ArticlesManager.UnitTests.UnitTests.Domain.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.Domain.UserCharts;
using ArticlesManager.Domain.UserCharts.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateUserChartTests
{
    private readonly Faker _faker;

    public CreateUserChartTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_userChart()
    {
        // Arrange + Act
        var fakeUserChart = FakeUserChart.Generate();

        // Assert
        fakeUserChart.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeUserChart = FakeUserChart.Generate();

        // Assert
        fakeUserChart.DomainEvents.Count.Should().Be(1);
        fakeUserChart.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserChartCreated));
    }
}