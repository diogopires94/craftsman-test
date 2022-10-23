namespace ArticlesManager.UnitTests.UnitTests.Domain.Articles.Features;

using ArticlesManager.SharedTestHelpers.Fakes.Article;
using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.Articles.Mappings;
using ArticlesManager.Domain.Articles.Features;
using ArticlesManager.Domain.Articles.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetArticleListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IArticleRepository> _articleRepository;

    public GetArticleListTests()
    {
        _articleRepository = new Mock<IArticleRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_article()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate();
        var fakeArticleTwo = FakeArticle.Generate();
        var fakeArticleThree = FakeArticle.Generate();
        var article = new List<Article>();
        article.Add(fakeArticleOne);
        article.Add(fakeArticleTwo);
        article.Add(fakeArticleThree);
        var mockDbData = article.AsQueryable().BuildMock();
        
        var queryParameters = new ArticleParametersDto() { PageSize = 1, PageNumber = 2 };

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_article_list_using_InternalReference()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.InternalReference, _ => "alpha")
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.InternalReference, _ => "bravo")
            .Generate());
        var queryParameters = new ArticleParametersDto() { Filters = $"InternalReference == {fakeArticleTwo.InternalReference}" };

        var articleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = articleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_article_list_using_SKU()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.SKU, _ => "alpha")
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.SKU, _ => "bravo")
            .Generate());
        var queryParameters = new ArticleParametersDto() { Filters = $"SKU == {fakeArticleTwo.SKU}" };

        var articleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = articleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
    }



























    [Test]
    public async Task can_filter_article_list_using_IsLowStock()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsLowStock, _ => false)
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsLowStock, _ => true)
            .Generate());
        var queryParameters = new ArticleParametersDto() { Filters = $"IsLowStock == {fakeArticleTwo.IsLowStock}" };

        var articleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = articleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_article_list_using_IsOutOfStock()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsOutOfStock, _ => false)
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsOutOfStock, _ => true)
            .Generate());
        var queryParameters = new ArticleParametersDto() { Filters = $"IsOutOfStock == {fakeArticleTwo.IsOutOfStock}" };

        var articleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = articleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_article_list_using_IsPublished()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsPublished, _ => false)
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsPublished, _ => true)
            .Generate());
        var queryParameters = new ArticleParametersDto() { Filters = $"IsPublished == {fakeArticleTwo.IsPublished}" };

        var articleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = articleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_article_list_using_IsOutlet()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsOutlet, _ => false)
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.IsOutlet, _ => true)
            .Generate());
        var queryParameters = new ArticleParametersDto() { Filters = $"IsOutlet == {fakeArticleTwo.IsOutlet}" };

        var articleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = articleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_article_by_InternalReference()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.InternalReference, _ => "alpha")
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.InternalReference, _ => "bravo")
            .Generate());
        var queryParameters = new ArticleParametersDto() { SortOrder = "-InternalReference" };

        var ArticleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = ArticleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_article_by_SKU()
    {
        //Arrange
        var fakeArticleOne = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.SKU, _ => "alpha")
            .Generate());
        var fakeArticleTwo = FakeArticle.Generate(new FakeArticleForCreationDto()
            .RuleFor(a => a.SKU, _ => "bravo")
            .Generate());
        var queryParameters = new ArticleParametersDto() { SortOrder = "-SKU" };

        var ArticleList = new List<Article>() { fakeArticleOne, fakeArticleTwo };
        var mockDbData = ArticleList.AsQueryable().BuildMock();

        _articleRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticleList.ArticleListQuery(queryParameters);
        var handler = new GetArticleList.Handler(_articleRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticleOne, options =>
                options.ExcludingMissingMembers());
    }


































}