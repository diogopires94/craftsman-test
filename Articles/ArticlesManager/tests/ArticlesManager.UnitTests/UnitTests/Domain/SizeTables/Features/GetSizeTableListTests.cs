namespace ArticlesManager.UnitTests.UnitTests.Domain.SizeTables.Features;

using ArticlesManager.SharedTestHelpers.Fakes.SizeTable;
using ArticlesManager.Domain.SizeTables;
using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.Domain.SizeTables.Mappings;
using ArticlesManager.Domain.SizeTables.Features;
using ArticlesManager.Domain.SizeTables.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetSizeTableListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<ISizeTableRepository> _sizeTableRepository;

    public GetSizeTableListTests()
    {
        _sizeTableRepository = new Mock<ISizeTableRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_sizeTable()
    {
        //Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate();
        var fakeSizeTableTwo = FakeSizeTable.Generate();
        var fakeSizeTableThree = FakeSizeTable.Generate();
        var sizeTable = new List<SizeTable>();
        sizeTable.Add(fakeSizeTableOne);
        sizeTable.Add(fakeSizeTableTwo);
        sizeTable.Add(fakeSizeTableThree);
        var mockDbData = sizeTable.AsQueryable().BuildMock();
        
        var queryParameters = new SizeTableParametersDto() { PageSize = 1, PageNumber = 2 };

        _sizeTableRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetSizeTableList.SizeTableListQuery(queryParameters);
        var handler = new GetSizeTableList.Handler(_sizeTableRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_sizetable_list_using_Name()
    {
        //Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.Name, _ => "alpha")
            .Generate());
        var fakeSizeTableTwo = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.Name, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableParametersDto() { Filters = $"Name == {fakeSizeTableTwo.Name}" };

        var sizeTableList = new List<SizeTable>() { fakeSizeTableOne, fakeSizeTableTwo };
        var mockDbData = sizeTableList.AsQueryable().BuildMock();

        _sizeTableRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableList.SizeTableListQuery(queryParameters);
        var handler = new GetSizeTableList.Handler(_sizeTableRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableTwo, options =>
                options.ExcludingMissingMembers());
    }





    [Test]
    public async Task can_get_sorted_list_of_sizetable_by_Name()
    {
        //Arrange
        var fakeSizeTableOne = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.Name, _ => "alpha")
            .Generate());
        var fakeSizeTableTwo = FakeSizeTable.Generate(new FakeSizeTableForCreationDto()
            .RuleFor(s => s.Name, _ => "bravo")
            .Generate());
        var queryParameters = new SizeTableParametersDto() { SortOrder = "-Name" };

        var SizeTableList = new List<SizeTable>() { fakeSizeTableOne, fakeSizeTableTwo };
        var mockDbData = SizeTableList.AsQueryable().BuildMock();

        _sizeTableRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSizeTableList.SizeTableListQuery(queryParameters);
        var handler = new GetSizeTableList.Handler(_sizeTableRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSizeTableOne, options =>
                options.ExcludingMissingMembers());
    }




}