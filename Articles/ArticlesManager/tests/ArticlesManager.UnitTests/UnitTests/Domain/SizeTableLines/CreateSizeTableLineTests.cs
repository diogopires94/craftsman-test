namespace ArticlesManager.UnitTests.UnitTests.Domain.SizeTableLines;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.Domain.SizeTableLines;
using ArticlesManager.Domain.SizeTableLines.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class CreateSizeTableLineTests
{
    private readonly Faker _faker;

    public CreateSizeTableLineTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_sizeTableLine()
    {
        // Arrange + Act
        var fakeSizeTableLine = FakeSizeTableLine.Generate();

        // Assert
        fakeSizeTableLine.Should().NotBeNull();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeSizeTableLine = FakeSizeTableLine.Generate();

        // Assert
        fakeSizeTableLine.DomainEvents.Count.Should().Be(1);
        fakeSizeTableLine.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(SizeTableLineCreated));
    }
}