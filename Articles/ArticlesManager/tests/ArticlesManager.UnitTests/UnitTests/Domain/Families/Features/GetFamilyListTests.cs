namespace ArticlesManager.UnitTests.UnitTests.Domain.Families.Features;

using ArticlesManager.SharedTestHelpers.Fakes.Family;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Domain.Families.Mappings;
using ArticlesManager.Domain.Families.Features;
using ArticlesManager.Domain.Families.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetFamilyListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IFamilyRepository> _familyRepository;

    public GetFamilyListTests()
    {
        _familyRepository = new Mock<IFamilyRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_family()
    {
        //Arrange
        var fakeFamilyOne = FakeFamily.Generate();
        var fakeFamilyTwo = FakeFamily.Generate();
        var fakeFamilyThree = FakeFamily.Generate();
        var family = new List<Family>();
        family.Add(fakeFamilyOne);
        family.Add(fakeFamilyTwo);
        family.Add(fakeFamilyThree);
        var mockDbData = family.AsQueryable().BuildMock();
        
        var queryParameters = new FamilyParametersDto() { PageSize = 1, PageNumber = 2 };

        _familyRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var handler = new GetFamilyList.Handler(_familyRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_family_list_using_Code()
    {
        //Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto()
            .RuleFor(f => f.Code, _ => "alpha")
            .Generate());
        var fakeFamilyTwo = FakeFamily.Generate(new FakeFamilyForCreationDto()
            .RuleFor(f => f.Code, _ => "bravo")
            .Generate());
        var queryParameters = new FamilyParametersDto() { Filters = $"Code == {fakeFamilyTwo.Code}" };

        var familyList = new List<Family>() { fakeFamilyOne, fakeFamilyTwo };
        var mockDbData = familyList.AsQueryable().BuildMock();

        _familyRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var handler = new GetFamilyList.Handler(_familyRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyTwo, options =>
                options.ExcludingMissingMembers());
    }



    [Test]
    public async Task can_get_sorted_list_of_family_by_Code()
    {
        //Arrange
        var fakeFamilyOne = FakeFamily.Generate(new FakeFamilyForCreationDto()
            .RuleFor(f => f.Code, _ => "alpha")
            .Generate());
        var fakeFamilyTwo = FakeFamily.Generate(new FakeFamilyForCreationDto()
            .RuleFor(f => f.Code, _ => "bravo")
            .Generate());
        var queryParameters = new FamilyParametersDto() { SortOrder = "-Code" };

        var FamilyList = new List<Family>() { fakeFamilyOne, fakeFamilyTwo };
        var mockDbData = FamilyList.AsQueryable().BuildMock();

        _familyRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var handler = new GetFamilyList.Handler(_familyRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyOne, options =>
                options.ExcludingMissingMembers());
    }


}