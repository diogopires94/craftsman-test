namespace ArticlesManager.UnitTests.UnitTests.Domain.Collections.Features;

using ArticlesManager.SharedTestHelpers.Fakes.Collection;
using ArticlesManager.Domain.Collections;
using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Domain.Collections.Mappings;
using ArticlesManager.Domain.Collections.Features;
using ArticlesManager.Domain.Collections.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetCollectionListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<ICollectionRepository> _collectionRepository;

    public GetCollectionListTests()
    {
        _collectionRepository = new Mock<ICollectionRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_collection()
    {
        //Arrange
        var fakeCollectionOne = FakeCollection.Generate();
        var fakeCollectionTwo = FakeCollection.Generate();
        var fakeCollectionThree = FakeCollection.Generate();
        var collection = new List<Collection>();
        collection.Add(fakeCollectionOne);
        collection.Add(fakeCollectionTwo);
        collection.Add(fakeCollectionThree);
        var mockDbData = collection.AsQueryable().BuildMock();
        
        var queryParameters = new CollectionParametersDto() { PageSize = 1, PageNumber = 2 };

        _collectionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetCollectionList.CollectionListQuery(queryParameters);
        var handler = new GetCollectionList.Handler(_collectionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_collection_list_using_Code()
    {
        //Arrange
        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto()
            .RuleFor(c => c.Code, _ => "alpha")
            .Generate());
        var fakeCollectionTwo = FakeCollection.Generate(new FakeCollectionForCreationDto()
            .RuleFor(c => c.Code, _ => "bravo")
            .Generate());
        var queryParameters = new CollectionParametersDto() { Filters = $"Code == {fakeCollectionTwo.Code}" };

        var collectionList = new List<Collection>() { fakeCollectionOne, fakeCollectionTwo };
        var mockDbData = collectionList.AsQueryable().BuildMock();

        _collectionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetCollectionList.CollectionListQuery(queryParameters);
        var handler = new GetCollectionList.Handler(_collectionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCollectionTwo, options =>
                options.ExcludingMissingMembers());
    }



    [Test]
    public async Task can_get_sorted_list_of_collection_by_Code()
    {
        //Arrange
        var fakeCollectionOne = FakeCollection.Generate(new FakeCollectionForCreationDto()
            .RuleFor(c => c.Code, _ => "alpha")
            .Generate());
        var fakeCollectionTwo = FakeCollection.Generate(new FakeCollectionForCreationDto()
            .RuleFor(c => c.Code, _ => "bravo")
            .Generate());
        var queryParameters = new CollectionParametersDto() { SortOrder = "-Code" };

        var CollectionList = new List<Collection>() { fakeCollectionOne, fakeCollectionTwo };
        var mockDbData = CollectionList.AsQueryable().BuildMock();

        _collectionRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetCollectionList.CollectionListQuery(queryParameters);
        var handler = new GetCollectionList.Handler(_collectionRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeCollectionTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCollectionOne, options =>
                options.ExcludingMissingMembers());
    }


}