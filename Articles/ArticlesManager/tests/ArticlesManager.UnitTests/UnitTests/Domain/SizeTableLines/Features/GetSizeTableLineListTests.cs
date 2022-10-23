namespace ArticlesManager.UnitTests.UnitTests.Domain.SizeTableLines.Features;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;
using ArticlesManager.Domain.SizeTableLines;
using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.Domain.SizeTableLines.Mappings;
using ArticlesManager.Domain.SizeTableLines.Features;
using ArticlesManager.Domain.SizeTableLines.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetSizeTableLineListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<ISizeTableLineRepository> _sizeTableLineRepository;

    public GetSizeTableLineListTests()
    {
        _sizeTableLineRepository = new Mock<ISizeTableLineRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_sizeTableLine()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate();
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate();
        var fakeSizeTableLineThree = FakeSizeTableLine.Generate();
        var sizeTableLine = new List<SizeTableLine>();
        sizeTableLine.Add(fakeSizeTableLineOne);
        sizeTableLine.Add(fakeSizeTableLineTwo);
        sizeTableLine.Add(fakeSizeTableLineThree);
        var mockDbData = sizeTableLine.AsQueryable().BuildMock();
        
        var queryParameters = new SizeTableLineParametersDto() { PageSize = 1, PageNumber = 2 };

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }



    [Test]
    public async Task can_filter_sizetableline_list_using_EU()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.EU, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.EU, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { Filters = $"EU == {fakeSizeTableLineTwo.EU}" };

        var sizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = sizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_sizetableline_list_using_US()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.US, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.US, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { Filters = $"US == {fakeSizeTableLineTwo.US}" };

        var sizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = sizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_sizetableline_list_using_UK()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.UK, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.UK, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { Filters = $"UK == {fakeSizeTableLineTwo.UK}" };

        var sizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = sizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_sizetableline_list_using_CM()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.CM, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.CM, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { Filters = $"CM == {fakeSizeTableLineTwo.CM}" };

        var sizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = sizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
    }



    [Test]
    public async Task can_get_sorted_list_of_sizetableline_by_EU()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.EU, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.EU, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { SortOrder = "-EU" };

        var SizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = SizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_sizetableline_by_US()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.US, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.US, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { SortOrder = "-US" };

        var SizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = SizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_sizetableline_by_UK()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.UK, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.UK, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { SortOrder = "-UK" };

        var SizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = SizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_sizetableline_by_CM()
    {
        //Arrange
        var fakeSizeTableLineOne = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.CM, _ => "alpha")
            .Generate());
        var fakeSizeTableLineTwo = FakeSizeTableLine.Generate(new FakeSizeTableLineForCreationDto()
            .RuleFor(s => s.CM, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableLineParametersDto() { SortOrder = "-CM" };

        var SizeTableLineList = new List<SizeTableLine>() { fakeSizeTableLineOne, fakeSizeTableLineTwo };
        var mockDbData = SizeTableLineList.AsQueryable().BuildMock();

        _sizeTableLineRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableLineList.SizeTableLineListQuery(queryParameters);
        var handler = new GetSizeTableLineList.Handler(_sizeTableLineRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableLineOne, options =>
                options.ExcludingMissingMembers());
    }
}