namespace ArticlesManager.UnitTests.UnitTests.Domain.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.Domain.SizeTables;
using ArticlesManager.Domain.SizeTables.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateSizeTableTests
{
    private readonly Faker _faker;

    public UpdateSizeTableTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_sizeTable()
    {
        // Arrange
        var fakeSizeTable = FakeSizeTable.Generate();
        var updatedSizeTable = new FakeSizeTableForUpdateDto().Generate();
        
        // Act
        fakeSizeTable.Update(updatedSizeTable);

        // Assert
        fakeSizeTable.Should().BeEquivalentTo(updatedSizeTable, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeSizeTable = FakeSizeTable.Generate();
        var updatedSizeTable = new FakeSizeTableForUpdateDto().Generate();
        fakeSizeTable.DomainEvents.Clear();
        
        // Act
        fakeSizeTable.Update(updatedSizeTable);

        // Assert
        fakeSizeTable.DomainEvents.Count.Should().Be(1);
        fakeSizeTable.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SizeTableUpdated));
    }
}