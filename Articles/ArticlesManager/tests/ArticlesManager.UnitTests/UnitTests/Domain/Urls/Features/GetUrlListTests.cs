namespace ArticlesManager.UnitTests.UnitTests.Domain.Urls.Features;

using ArticlesManager.SharedTestHelpers.Fakes.Url;
using ArticlesManager.Domain.Urls;
using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Domain.Urls.Mappings;
using ArticlesManager.Domain.Urls.Features;
using ArticlesManager.Domain.Urls.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetUrlListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IUrlRepository> _urlRepository;

    public GetUrlListTests()
    {
        _urlRepository = new Mock<IUrlRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_url()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate();
        var fakeUrlTwo = FakeUrl.Generate();
        var fakeUrlThree = FakeUrl.Generate();
        var url = new List<Url>();
        url.Add(fakeUrlOne);
        url.Add(fakeUrlTwo);
        url.Add(fakeUrlThree);
        var mockDbData = url.AsQueryable().BuildMock();
        
        var queryParameters = new UrlParametersDto() { PageSize = 1, PageNumber = 2 };

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_url_list_using_UrlValue()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.UrlValue, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.UrlValue, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { Filters = $"UrlValue == {fakeUrlTwo.UrlValue}" };

        var urlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = urlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_url_list_using_PageTitle()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.PageTitle, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.PageTitle, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { Filters = $"PageTitle == {fakeUrlTwo.PageTitle}" };

        var urlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = urlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_url_list_using_MetaDescription()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaDescription, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaDescription, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { Filters = $"MetaDescription == {fakeUrlTwo.MetaDescription}" };

        var urlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = urlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_url_list_using_MetaName()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaName, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaName, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { Filters = $"MetaName == {fakeUrlTwo.MetaName}" };

        var urlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = urlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_url_by_UrlValue()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.UrlValue, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.UrlValue, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { SortOrder = "-UrlValue" };

        var UrlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = UrlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_url_by_PageTitle()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.PageTitle, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.PageTitle, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { SortOrder = "-PageTitle" };

        var UrlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = UrlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_url_by_MetaDescription()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaDescription, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaDescription, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { SortOrder = "-MetaDescription" };

        var UrlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = UrlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_url_by_MetaName()
    {
        //Arrange
        var fakeUrlOne = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaName, _ => "alpha")
            .Generate());
        var fakeUrlTwo = FakeUrl.Generate(new FakeUrlForCreationDto()
            .RuleFor(u => u.MetaName, _ => "bravo")
            .Generate());
        var queryParameters = new UrlParametersDto() { SortOrder = "-MetaName" };

        var UrlList = new List<Url>() { fakeUrlOne, fakeUrlTwo };
        var mockDbData = UrlList.AsQueryable().BuildMock();

        _urlRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetUrlList.UrlListQuery(queryParameters);
        var handler = new GetUrlList.Handler(_urlRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeUrlOne, options =>
                options.ExcludingMissingMembers());
    }
}