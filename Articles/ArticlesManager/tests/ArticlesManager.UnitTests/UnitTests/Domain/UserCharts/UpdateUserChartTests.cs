namespace ArticlesManager.UnitTests.UnitTests.Domain.UserCharts;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.Domain.UserCharts;
using ArticlesManager.Domain.UserCharts.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateUserChartTests
{
    private readonly Faker _faker;

    public UpdateUserChartTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_userChart()
    {
        // Arrange
        var fakeUserChart = FakeUserChart.Generate();
        var updatedUserChart = new FakeUserChartForUpdateDto().Generate();
        
        // Act
        fakeUserChart.Update(updatedUserChart);

        // Assert
        fakeUserChart.Should().BeEquivalentTo(updatedUserChart, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUserChart = FakeUserChart.Generate();
        var updatedUserChart = new FakeUserChartForUpdateDto().Generate();
        fakeUserChart.DomainEvents.Clear();
        
        // Act
        fakeUserChart.Update(updatedUserChart);

        // Assert
        fakeUserChart.DomainEvents.Count.Should().Be(1);
        fakeUserChart.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserChartUpdated));
    }
}