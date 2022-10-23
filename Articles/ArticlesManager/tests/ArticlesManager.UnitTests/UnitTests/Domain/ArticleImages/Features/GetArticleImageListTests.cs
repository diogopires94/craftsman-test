namespace ArticlesManager.UnitTests.UnitTests.Domain.ArticleImages.Features;

using ArticlesManager.SharedTestHelpers.Fakes.ArticleImage;
using ArticlesManager.Domain.ArticleImages;
using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.Domain.ArticleImages.Mappings;
using ArticlesManager.Domain.ArticleImages.Features;
using ArticlesManager.Domain.ArticleImages.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using NUnit.Framework;

public class GetArticleImageListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = new Mapper();
    private readonly Mock<IArticleImageRepository> _articleImageRepository;

    public GetArticleImageListTests()
    {
        _articleImageRepository = new Mock<IArticleImageRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_articleImage()
    {
        //Arrange
        var fakeArticleImageOne = FakeArticleImage.Generate();
        var fakeArticleImageTwo = FakeArticleImage.Generate();
        var fakeArticleImageThree = FakeArticleImage.Generate();
        var articleImage = new List<ArticleImage>();
        articleImage.Add(fakeArticleImageOne);
        articleImage.Add(fakeArticleImageTwo);
        articleImage.Add(fakeArticleImageThree);
        var mockDbData = articleImage.AsQueryable().BuildMock();
        
        var queryParameters = new ArticleImageParametersDto() { PageSize = 1, PageNumber = 2 };

        _articleImageRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetArticleImageList.ArticleImageListQuery(queryParameters);
        var handler = new GetArticleImageList.Handler(_articleImageRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }




}