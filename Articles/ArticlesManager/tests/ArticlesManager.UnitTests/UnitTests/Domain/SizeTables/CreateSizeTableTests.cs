namespace ArticlesManager.UnitTests.UnitTests.Domain.SizeTables;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.Domain.SizeTables;
using ArticlesManager.Domain.SizeTables.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateSizeTableTests
{
    private readonly Faker _faker;

    public CreateSizeTableTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_sizeTable()
    {
        // Arrange + Act
        var fakeSizeTable = FakeSizeTable.Generate();

        // Assert
        fakeSizeTable.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeSizeTable = FakeSizeTable.Generate();

        // Assert
        fakeSizeTable.DomainEvents.Count.Should().Be(1);
        fakeSizeTable.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SizeTableCreated));
    }
}