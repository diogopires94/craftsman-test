namespace ArticlesManager.UnitTests.UnitTests.Domain.Promotions.Features;

using ArticlesManager.SharedTestHelpers.Fakes.Promotion;
using ArticlesManager.Domain.Promotions;
using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.Domain.Promotions.Mappings;
using ArticlesManager.Domain.Promotions.Features;
using ArticlesManager.Domain.Promotions.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetPromotionListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IPromotionRepository> _promotionRepository;

    public GetPromotionListTests()
    {
        _promotionRepository = new Mock<IPromotionRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_promotion()
    {
        //Arrange
        var fakePromotionOne = FakePromotion.Generate();
        var fakePromotionTwo = FakePromotion.Generate();
        var fakePromotionThree = FakePromotion.Generate();
        var promotion = new List<Promotion>();
        promotion.Add(fakePromotionOne);
        promotion.Add(fakePromotionTwo);
        promotion.Add(fakePromotionThree);
        var mockDbData = promotion.AsQueryable().BuildMock();
        
        var queryParameters = new PromotionParametersDto() { PageSize = 1, PageNumber = 2 };

        _promotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetPromotionList.PromotionListQuery(queryParameters);
        var handler = new GetPromotionList.Handler(_promotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_promotion_list_using_Name()
    {
        //Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Name, _ => "alpha")
            .Generate());
        var fakePromotionTwo = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Name, _ => "bravo")
            .Generate());
        var queryParameters = new PromotionParametersDto() { Filters = $"Name == {fakePromotionTwo.Name}" };

        var promotionList = new List<Promotion>() { fakePromotionOne, fakePromotionTwo };
        var mockDbData = promotionList.AsQueryable().BuildMock();

        _promotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetPromotionList.PromotionListQuery(queryParameters);
        var handler = new GetPromotionList.Handler(_promotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakePromotionTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_promotion_list_using_Filter()
    {
        //Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Filter, _ => "alpha")
            .Generate());
        var fakePromotionTwo = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Filter, _ => "bravo")
            .Generate());
        var queryParameters = new PromotionParametersDto() { Filters = $"Filter == {fakePromotionTwo.Filter}" };

        var promotionList = new List<Promotion>() { fakePromotionOne, fakePromotionTwo };
        var mockDbData = promotionList.AsQueryable().BuildMock();

        _promotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetPromotionList.PromotionListQuery(queryParameters);
        var handler = new GetPromotionList.Handler(_promotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakePromotionTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_promotion_by_Name()
    {
        //Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Name, _ => "alpha")
            .Generate());
        var fakePromotionTwo = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Name, _ => "bravo")
            .Generate());
        var queryParameters = new PromotionParametersDto() { SortOrder = "-Name" };

        var PromotionList = new List<Promotion>() { fakePromotionOne, fakePromotionTwo };
        var mockDbData = PromotionList.AsQueryable().BuildMock();

        _promotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetPromotionList.PromotionListQuery(queryParameters);
        var handler = new GetPromotionList.Handler(_promotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakePromotionTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakePromotionOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_promotion_by_Filter()
    {
        //Arrange
        var fakePromotionOne = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Filter, _ => "alpha")
            .Generate());
        var fakePromotionTwo = FakePromotion.Generate(new FakePromotionForCreationDto()
            .RuleFor(p => p.Filter, _ => "bravo")
            .Generate());
        var queryParameters = new PromotionParametersDto() { SortOrder = "-Filter" };

        var PromotionList = new List<Promotion>() { fakePromotionOne, fakePromotionTwo };
        var mockDbData = PromotionList.AsQueryable().BuildMock();

        _promotionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetPromotionList.PromotionListQuery(queryParameters);
        var handler = new GetPromotionList.Handler(_promotionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakePromotionTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakePromotionOne, options =>
                options.ExcludingMissingMembers());
    }
}