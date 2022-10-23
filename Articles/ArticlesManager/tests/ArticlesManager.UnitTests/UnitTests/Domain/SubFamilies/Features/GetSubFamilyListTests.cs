namespace ArticlesManager.UnitTests.UnitTests.Domain.SubFamilies.Features;

using ArticlesManager.SharedTestHelpers.Fakes.SubFamily;
using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Domain.SubFamilies.Mappings;
using ArticlesManager.Domain.SubFamilies.Features;
using ArticlesManager.Domain.SubFamilies.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetSubFamilyListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<ISubFamilyRepository> _subFamilyRepository;

    public GetSubFamilyListTests()
    {
        _subFamilyRepository = new Mock<ISubFamilyRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_subFamily()
    {
        //Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate();
        var fakeSubFamilyTwo = FakeSubFamily.Generate();
        var fakeSubFamilyThree = FakeSubFamily.Generate();
        var subFamily = new List<SubFamily>();
        subFamily.Add(fakeSubFamilyOne);
        subFamily.Add(fakeSubFamilyTwo);
        subFamily.Add(fakeSubFamilyThree);
        var mockDbData = subFamily.AsQueryable().BuildMock();
        
        var queryParameters = new SubFamilyParametersDto() { PageSize = 1, PageNumber = 2 };

        _subFamilyRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetSubFamilyList.SubFamilyListQuery(queryParameters);
        var handler = new GetSubFamilyList.Handler(_subFamilyRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_subfamily_list_using_Code()
    {
        //Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto()
            .RuleFor(s => s.Code, _ => "alpha")
            .Generate());
        var fakeSubFamilyTwo = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto()
            .RuleFor(s => s.Code, _ => "bravo")
            .Generate());
        var queryParameters = new SubFamilyParametersDto() { Filters = $"Code == {fakeSubFamilyTwo.Code}" };

        var subFamilyList = new List<SubFamily>() { fakeSubFamilyOne, fakeSubFamilyTwo };
        var mockDbData = subFamilyList.AsQueryable().BuildMock();

        _subFamilyRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSubFamilyList.SubFamilyListQuery(queryParameters);
        var handler = new GetSubFamilyList.Handler(_subFamilyRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSubFamilyTwo, options =>
                options.ExcludingMissingMembers());
    }



    [Test]
    public async Task can_get_sorted_list_of_subfamily_by_Code()
    {
        //Arrange
        var fakeSubFamilyOne = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto()
            .RuleFor(s => s.Code, _ => "alpha")
            .Generate());
        var fakeSubFamilyTwo = FakeSubFamily.Generate(new FakeSubFamilyForCreationDto()
            .RuleFor(s => s.Code, _ => "bravo")
            .Generate());
        var queryParameters = new SubFamilyParametersDto() { SortOrder = "-Code" };

        var SubFamilyList = new List<SubFamily>() { fakeSubFamilyOne, fakeSubFamilyTwo };
        var mockDbData = SubFamilyList.AsQueryable().BuildMock();

        _subFamilyRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetSubFamilyList.SubFamilyListQuery(queryParameters);
        var handler = new GetSubFamilyList.Handler(_subFamilyRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeSubFamilyTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeSubFamilyOne, options =>
                options.ExcludingMissingMembers());
    }


}