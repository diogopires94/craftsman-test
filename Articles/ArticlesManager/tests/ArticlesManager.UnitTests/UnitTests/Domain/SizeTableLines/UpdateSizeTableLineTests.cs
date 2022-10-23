namespace ArticlesManager.UnitTests.UnitTests.Domain.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.Domain.SizeTableLines;
using ArticlesManager.Domain.SizeTableLines.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateSizeTableLineTests
{
    private readonly Faker _faker;

    public UpdateSizeTableLineTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_sizeTableLine()
    {
        // Arrange
        var fakeSizeTableLine = FakeSizeTableLine.Generate();
        var updatedSizeTableLine = new FakeSizeTableLineForUpdateDto().Generate();
        
        // Act
        fakeSizeTableLine.Update(updatedSizeTableLine);

        // Assert
        fakeSizeTableLine.Should().BeEquivalentTo(updatedSizeTableLine, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeSizeTableLine = FakeSizeTableLine.Generate();
        var updatedSizeTableLine = new FakeSizeTableLineForUpdateDto().Generate();
        fakeSizeTableLine.DomainEvents.Clear();
        
        // Act
        fakeSizeTableLine.Update(updatedSizeTableLine);

        // Assert
        fakeSizeTableLine.DomainEvents.Count.Should().Be(1);
        fakeSizeTableLine.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SizeTableLineUpdated));
    }
}