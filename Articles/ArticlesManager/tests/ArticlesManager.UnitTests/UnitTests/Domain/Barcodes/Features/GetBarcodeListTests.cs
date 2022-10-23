namespace ArticlesManager.UnitTests.UnitTests.Domain.Barcodes.Features;

using ArticlesManager.SharedTestHelpers.Fakes.Barcode;
using ArticlesManager.Domain.Barcodes;
using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Barcodes.Mappings;
using ArticlesManager.Domain.Barcodes.Features;
using ArticlesManager.Domain.Barcodes.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetBarcodeListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IBarcodeRepository> _barcodeRepository;

    public GetBarcodeListTests()
    {
        _barcodeRepository = new Mock<IBarcodeRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_barcode()
    {
        //Arrange
        var fakeBarcodeOne = FakeBarcode.Generate();
        var fakeBarcodeTwo = FakeBarcode.Generate();
        var fakeBarcodeThree = FakeBarcode.Generate();
        var barcode = new List<Barcode>();
        barcode.Add(fakeBarcodeOne);
        barcode.Add(fakeBarcodeTwo);
        barcode.Add(fakeBarcodeThree);
        var mockDbData = barcode.AsQueryable().BuildMock();
        
        var queryParameters = new BarcodeParametersDto() { PageSize = 1, PageNumber = 2 };

        _barcodeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var handler = new GetBarcodeList.Handler(_barcodeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_barcode_list_using_BarcodeValue()
    {
        //Arrange
        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.BarcodeValue, _ => "alpha")
            .Generate());
        var fakeBarcodeTwo = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.BarcodeValue, _ => "bravo")
            .Generate());
        var queryParameters = new BarcodeParametersDto() { Filters = $"BarcodeValue == {fakeBarcodeTwo.BarcodeValue}" };

        var barcodeList = new List<Barcode>() { fakeBarcodeOne, fakeBarcodeTwo };
        var mockDbData = barcodeList.AsQueryable().BuildMock();

        _barcodeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var handler = new GetBarcodeList.Handler(_barcodeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeTwo, options =>
                options.ExcludingMissingMembers());
    }











    [Test]
    public async Task can_filter_barcode_list_using_StockQuantity()
    {
        //Arrange
        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.StockQuantity, _ => 1)
            .Generate());
        var fakeBarcodeTwo = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.StockQuantity, _ => 2)
            .Generate());
        var queryParameters = new BarcodeParametersDto() { Filters = $"StockQuantity == {fakeBarcodeTwo.StockQuantity}" };

        var barcodeList = new List<Barcode>() { fakeBarcodeOne, fakeBarcodeTwo };
        var mockDbData = barcodeList.AsQueryable().BuildMock();

        _barcodeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var handler = new GetBarcodeList.Handler(_barcodeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_barcode_list_using_ReservedQuantity()
    {
        //Arrange
        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ReservedQuantity, _ => 1)
            .Generate());
        var fakeBarcodeTwo = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ReservedQuantity, _ => 2)
            .Generate());
        var queryParameters = new BarcodeParametersDto() { Filters = $"ReservedQuantity == {fakeBarcodeTwo.ReservedQuantity}" };

        var barcodeList = new List<Barcode>() { fakeBarcodeOne, fakeBarcodeTwo };
        var mockDbData = barcodeList.AsQueryable().BuildMock();

        _barcodeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var handler = new GetBarcodeList.Handler(_barcodeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_barcode_by_BarcodeValue()
    {
        //Arrange
        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.BarcodeValue, _ => "alpha")
            .Generate());
        var fakeBarcodeTwo = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.BarcodeValue, _ => "bravo")
            .Generate());
        var queryParameters = new BarcodeParametersDto() { SortOrder = "-BarcodeValue" };

        var BarcodeList = new List<Barcode>() { fakeBarcodeOne, fakeBarcodeTwo };
        var mockDbData = BarcodeList.AsQueryable().BuildMock();

        _barcodeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var handler = new GetBarcodeList.Handler(_barcodeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeOne, options =>
                options.ExcludingMissingMembers());
    }











    [Test]
    public async Task can_get_sorted_list_of_barcode_by_StockQuantity()
    {
        //Arrange
        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.StockQuantity, _ => 1)
            .Generate());
        var fakeBarcodeTwo = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.StockQuantity, _ => 2)
            .Generate());
        var queryParameters = new BarcodeParametersDto() { SortOrder = "-StockQuantity" };

        var BarcodeList = new List<Barcode>() { fakeBarcodeOne, fakeBarcodeTwo };
        var mockDbData = BarcodeList.AsQueryable().BuildMock();

        _barcodeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var handler = new GetBarcodeList.Handler(_barcodeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_barcode_by_ReservedQuantity()
    {
        //Arrange
        var fakeBarcodeOne = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ReservedQuantity, _ => 1)
            .Generate());
        var fakeBarcodeTwo = FakeBarcode.Generate(new FakeBarcodeForCreationDto()
            .RuleFor(b => b.ReservedQuantity, _ => 2)
            .Generate());
        var queryParameters = new BarcodeParametersDto() { SortOrder = "-ReservedQuantity" };

        var BarcodeList = new List<Barcode>() { fakeBarcodeOne, fakeBarcodeTwo };
        var mockDbData = BarcodeList.AsQueryable().BuildMock();

        _barcodeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBarcodeList.BarcodeListQuery(queryParameters);
        var handler = new GetBarcodeList.Handler(_barcodeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBarcodeOne, options =>
                options.ExcludingMissingMembers());
    }
}