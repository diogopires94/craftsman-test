namespace ArticlesManager.UnitTests.UnitTests.Domain.HomePageHighlights.Features;

using ArticlesManager.SharedTestHelpers.Fakes.HomePageHighlight;
using ArticlesManager.Domain.HomePageHighlights;
using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.Domain.HomePageHighlights.Mappings;
using ArticlesManager.Domain.HomePageHighlights.Features;
using ArticlesManager.Domain.HomePageHighlights.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetHomePageHighlightListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IHomePageHighlightRepository> _homePageHighlightRepository;

    public GetHomePageHighlightListTests()
    {
        _homePageHighlightRepository = new Mock<IHomePageHighlightRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_homePageHighlight()
    {
        //Arrange
        var fakeHomePageHighlightOne = FakeHomePageHighlight.Generate();
        var fakeHomePageHighlightTwo = FakeHomePageHighlight.Generate();
        var fakeHomePageHighlightThree = FakeHomePageHighlight.Generate();
        var homePageHighlight = new List<HomePageHighlight>();
        homePageHighlight.Add(fakeHomePageHighlightOne);
        homePageHighlight.Add(fakeHomePageHighlightTwo);
        homePageHighlight.Add(fakeHomePageHighlightThree);
        var mockDbData = homePageHighlight.AsQueryable().BuildMock();
        
        var queryParameters = new HomePageHighlightParametersDto() { PageSize = 1, PageNumber = 2 };

        _homePageHighlightRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetHomePageHighlightList.HomePageHighlightListQuery(queryParameters);
        var handler = new GetHomePageHighlightList.Handler(_homePageHighlightRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }







    [Test]
    public async Task can_filter_homepagehighlight_list_using_Name()
    {
        //Arrange
        var fakeHomePageHighlightOne = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Name, _ => "alpha")
            .Generate());
        var fakeHomePageHighlightTwo = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Name, _ => "bravo")
            .Generate());
        var queryParameters = new HomePageHighlightParametersDto() { Filters = $"Name == {fakeHomePageHighlightTwo.Name}" };

        var homePageHighlightList = new List<HomePageHighlight>() { fakeHomePageHighlightOne, fakeHomePageHighlightTwo };
        var mockDbData = homePageHighlightList.AsQueryable().BuildMock();

        _homePageHighlightRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHomePageHighlightList.HomePageHighlightListQuery(queryParameters);
        var handler = new GetHomePageHighlightList.Handler(_homePageHighlightRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHomePageHighlightTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_homepagehighlight_list_using_Order()
    {
        //Arrange
        var fakeHomePageHighlightOne = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Order, _ => 1)
            .Generate());
        var fakeHomePageHighlightTwo = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Order, _ => 2)
            .Generate());
        var queryParameters = new HomePageHighlightParametersDto() { Filters = $"Order == {fakeHomePageHighlightTwo.Order}" };

        var homePageHighlightList = new List<HomePageHighlight>() { fakeHomePageHighlightOne, fakeHomePageHighlightTwo };
        var mockDbData = homePageHighlightList.AsQueryable().BuildMock();

        _homePageHighlightRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHomePageHighlightList.HomePageHighlightListQuery(queryParameters);
        var handler = new GetHomePageHighlightList.Handler(_homePageHighlightRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHomePageHighlightTwo, options =>
                options.ExcludingMissingMembers());
    }







    [Test]
    public async Task can_get_sorted_list_of_homepagehighlight_by_Name()
    {
        //Arrange
        var fakeHomePageHighlightOne = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Name, _ => "alpha")
            .Generate());
        var fakeHomePageHighlightTwo = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Name, _ => "bravo")
            .Generate());
        var queryParameters = new HomePageHighlightParametersDto() { SortOrder = "-Name" };

        var HomePageHighlightList = new List<HomePageHighlight>() { fakeHomePageHighlightOne, fakeHomePageHighlightTwo };
        var mockDbData = HomePageHighlightList.AsQueryable().BuildMock();

        _homePageHighlightRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHomePageHighlightList.HomePageHighlightListQuery(queryParameters);
        var handler = new GetHomePageHighlightList.Handler(_homePageHighlightRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeHomePageHighlightTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHomePageHighlightOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_homepagehighlight_by_Order()
    {
        //Arrange
        var fakeHomePageHighlightOne = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Order, _ => 1)
            .Generate());
        var fakeHomePageHighlightTwo = FakeHomePageHighlight.Generate(new FakeHomePageHighlightForCreationDto()
            .RuleFor(h => h.Order, _ => 2)
            .Generate());
        var queryParameters = new HomePageHighlightParametersDto() { SortOrder = "-Order" };

        var HomePageHighlightList = new List<HomePageHighlight>() { fakeHomePageHighlightOne, fakeHomePageHighlightTwo };
        var mockDbData = HomePageHighlightList.AsQueryable().BuildMock();

        _homePageHighlightRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetHomePageHighlightList.HomePageHighlightListQuery(queryParameters);
        var handler = new GetHomePageHighlightList.Handler(_homePageHighlightRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeHomePageHighlightTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeHomePageHighlightOne, options =>
                options.ExcludingMissingMembers());
    }
}