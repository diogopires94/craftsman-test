namespace ArticlesManager.UnitTests.UnitTests.Domain.Brands.Features;

using ArticlesManager.SharedTestHelpers.Fakes.Brand;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Domain.Brands.Mappings;
using ArticlesManager.Domain.Brands.Features;
using ArticlesManager.Domain.Brands.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetBrandListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IBrandRepository> _brandRepository;

    public GetBrandListTests()
    {
        _brandRepository = new Mock<IBrandRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_brand()
    {
        //Arrange
        var fakeBrandOne = FakeBrand.Generate();
        var fakeBrandTwo = FakeBrand.Generate();
        var fakeBrandThree = FakeBrand.Generate();
        var brand = new List<Brand>();
        brand.Add(fakeBrandOne);
        brand.Add(fakeBrandTwo);
        brand.Add(fakeBrandThree);
        var mockDbData = brand.AsQueryable().BuildMock();
        
        var queryParameters = new BrandParametersDto() { PageSize = 1, PageNumber = 2 };

        _brandRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetBrandList.BrandListQuery(queryParameters);
        var handler = new GetBrandList.Handler(_brandRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_brand_list_using_Code()
    {
        //Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto()
            .RuleFor(b => b.Code, _ => "alpha")
            .Generate());
        var fakeBrandTwo = FakeBrand.Generate(new FakeBrandForCreationDto()
            .RuleFor(b => b.Code, _ => "bravo")
            .Generate());
        var queryParameters = new BrandParametersDto() { Filters = $"Code == {fakeBrandTwo.Code}" };

        var brandList = new List<Brand>() { fakeBrandOne, fakeBrandTwo };
        var mockDbData = brandList.AsQueryable().BuildMock();

        _brandRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBrandList.BrandListQuery(queryParameters);
        var handler = new GetBrandList.Handler(_brandRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBrandTwo, options =>
                options.ExcludingMissingMembers());
    }





    [Test]
    public async Task can_get_sorted_list_of_brand_by_Code()
    {
        //Arrange
        var fakeBrandOne = FakeBrand.Generate(new FakeBrandForCreationDto()
            .RuleFor(b => b.Code, _ => "alpha")
            .Generate());
        var fakeBrandTwo = FakeBrand.Generate(new FakeBrandForCreationDto()
            .RuleFor(b => b.Code, _ => "bravo")
            .Generate());
        var queryParameters = new BrandParametersDto() { SortOrder = "-Code" };

        var BrandList = new List<Brand>() { fakeBrandOne, fakeBrandTwo };
        var mockDbData = BrandList.AsQueryable().BuildMock();

        _brandRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetBrandList.BrandListQuery(queryParameters);
        var handler = new GetBrandList.Handler(_brandRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeBrandTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBrandOne, options =>
                options.ExcludingMissingMembers());
    }




}