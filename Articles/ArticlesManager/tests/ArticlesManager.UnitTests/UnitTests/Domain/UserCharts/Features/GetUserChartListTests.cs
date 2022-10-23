namespace ArticlesManager.UnitTests.UnitTests.Domain.UserCharts.Features;

using ArticlesManager.SharedTestHelpers.Fakes.UserChart;
using ArticlesManager.Domain.UserCharts;
using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Domain.UserCharts.Mappings;
using ArticlesManager.Domain.UserCharts.Features;
using ArticlesManager.Domain.UserCharts.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetUserChartListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IUserChartRepository> _userChartRepository;

    public GetUserChartListTests()
    {
        _userChartRepository = new Mock<IUserChartRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_userChart()
    {
        //Arrange
        var fakeUserChartOne = FakeUserChart.Generate();
        var fakeUserChartTwo = FakeUserChart.Generate();
        var fakeUserChartThree = FakeUserChart.Generate();
        var userChart = new List<UserChart>();
        userChart.Add(fakeUserChartOne);
        userChart.Add(fakeUserChartTwo);
        userChart.Add(fakeUserChartThree);
        var mockDbData = userChart.AsQueryable().BuildMock();
        
        var queryParameters = new UserChartParametersDto() { PageSize = 1, PageNumber = 2 };

        _userChartRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetUserChartList.UserChartListQuery(queryParameters);
        var handler = new GetUserChartList.Handler(_userChartRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }





    [Test]
    public async Task can_filter_userchart_list_using_Quantity()
    {
        //Arrange
        var fakeUserChartOne = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.Quantity, _ => 1)
            .Generate());
        var fakeUserChartTwo = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.Quantity, _ => 2)
            .Generate());
        var queryParameters = new UserChartParametersDto() { Filters = $"Quantity == {fakeUserChartTwo.Quantity}" };

        var userChartList = new List<UserChart>() { fakeUserChartOne, fakeUserChartTwo };
        var mockDbData = userChartList.AsQueryable().BuildMock();

        _userChartRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUserChartList.UserChartListQuery(queryParameters);
        var handler = new GetUserChartList.Handler(_userChartRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUserChartTwo, options =>
                options.ExcludingMissingMembers());
    }





    [Test]
    public async Task can_get_sorted_list_of_userchart_by_Quantity()
    {
        //Arrange
        var fakeUserChartOne = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.Quantity, _ => 1)
            .Generate());
        var fakeUserChartTwo = FakeUserChart.Generate(new FakeUserChartForCreationDto()
            .RuleFor(u => u.Quantity, _ => 2)
            .Generate());
        var queryParameters = new UserChartParametersDto() { SortOrder = "-Quantity" };

        var UserChartList = new List<UserChart>() { fakeUserChartOne, fakeUserChartTwo };
        var mockDbData = UserChartList.AsQueryable().BuildMock();

        _userChartRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUserChartList.UserChartListQuery(queryParameters);
        var handler = new GetUserChartList.Handler(_userChartRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeUserChartTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUserChartOne, options =>
                options.ExcludingMissingMembers());
    }
}