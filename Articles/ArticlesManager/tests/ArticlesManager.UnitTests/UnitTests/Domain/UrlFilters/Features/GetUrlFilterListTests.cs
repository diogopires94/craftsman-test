namespace ArticlesManager.UnitTests.UnitTests.Domain.UrlFilters.Features;

using ArticlesManager.SharedTestHelpers.Fakes.UrlFilter;
using ArticlesManager.Domain.UrlFilters;
using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.Domain.UrlFilters.Mappings;
using ArticlesManager.Domain.UrlFilters.Features;
using ArticlesManager.Domain.UrlFilters.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetUrlFilterListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IUrlFilterRepository> _urlFilterRepository;

    public GetUrlFilterListTests()
    {
        _urlFilterRepository = new Mock<IUrlFilterRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_urlFilter()
    {
        //Arrange
        var fakeUrlFilterOne = FakeUrlFilter.Generate();
        var fakeUrlFilterTwo = FakeUrlFilter.Generate();
        var fakeUrlFilterThree = FakeUrlFilter.Generate();
        var urlFilter = new List<UrlFilter>();
        urlFilter.Add(fakeUrlFilterOne);
        urlFilter.Add(fakeUrlFilterTwo);
        urlFilter.Add(fakeUrlFilterThree);
        var mockDbData = urlFilter.AsQueryable().BuildMock();
        
        var queryParameters = new UrlFilterParametersDto() { PageSize = 1, PageNumber = 2 };

        _urlFilterRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetUrlFilterList.UrlFilterListQuery(queryParameters);
        var handler = new GetUrlFilterList.Handler(_urlFilterRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }




















}