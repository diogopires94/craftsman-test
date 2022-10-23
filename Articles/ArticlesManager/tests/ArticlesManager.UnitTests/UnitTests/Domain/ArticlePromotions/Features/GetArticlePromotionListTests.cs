namespace ArticlesManager.UnitTests.UnitTests.Domain.ArticlePromotions.Features;

using ArticlesManager.SharedTestHelpers.Fakes.ArticlePromotion;
using ArticlesManager.Domain.ArticlePromotions;
using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.Domain.ArticlePromotions.Mappings;
using ArticlesManager.Domain.ArticlePromotions.Features;
using ArticlesManager.Domain.ArticlePromotions.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetArticlePromotionListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IArticlePromotionRepository> _articlePromotionRepository;

    public GetArticlePromotionListTests()
    {
        _articlePromotionRepository = new Mock<IArticlePromotionRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_articlePromotion()
    {
        //Arrange
        var fakeArticlePromotionOne = FakeArticlePromotion.Generate();
        var fakeArticlePromotionTwo = FakeArticlePromotion.Generate();
        var fakeArticlePromotionThree = FakeArticlePromotion.Generate();
        var articlePromotion = new List<ArticlePromotion>();
        articlePromotion.Add(fakeArticlePromotionOne);
        articlePromotion.Add(fakeArticlePromotionTwo);
        articlePromotion.Add(fakeArticlePromotionThree);
        var mockDbData = articlePromotion.AsQueryable().BuildMock();
        
        var queryParameters = new ArticlePromotionParametersDto() { PageSize = 1, PageNumber = 2 };

        _articlePromotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetArticlePromotionList.ArticlePromotionListQuery(queryParameters);
        var handler = new GetArticlePromotionList.Handler(_articlePromotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_articlepromotion_list_using_Discount()
    {
        //Arrange
        var fakeArticlePromotionOne = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.Discount, _ => 1)
            .Generate());
        var fakeArticlePromotionTwo = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.Discount, _ => 2)
            .Generate());
        var queryParameters = new ArticlePromotionParametersDto() { Filters = $"Discount == {fakeArticlePromotionTwo.Discount}" };

        var articlePromotionList = new List<ArticlePromotion>() { fakeArticlePromotionOne, fakeArticlePromotionTwo };
        var mockDbData = articlePromotionList.AsQueryable().BuildMock();

        _articlePromotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticlePromotionList.ArticlePromotionListQuery(queryParameters);
        var handler = new GetArticlePromotionList.Handler(_articlePromotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticlePromotionTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_articlepromotion_by_Discount()
    {
        //Arrange
        var fakeArticlePromotionOne = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.Discount, _ => 1)
            .Generate());
        var fakeArticlePromotionTwo = FakeArticlePromotion.Generate(new FakeArticlePromotionForCreationDto()
            .RuleFor(a => a.Discount, _ => 2)
            .Generate());
        var queryParameters = new ArticlePromotionParametersDto() { SortOrder = "-Discount" };

        var ArticlePromotionList = new List<ArticlePromotion>() { fakeArticlePromotionOne, fakeArticlePromotionTwo };
        var mockDbData = ArticlePromotionList.AsQueryable().BuildMock();

        _articlePromotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetArticlePromotionList.ArticlePromotionListQuery(queryParameters);
        var handler = new GetArticlePromotionList.Handler(_articlePromotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticlePromotionTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeArticlePromotionOne, options =>
                options.ExcludingMissingMembers());
    }
}