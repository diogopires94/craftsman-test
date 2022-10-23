namespace ArticlesManager.Domain.HomePageHighlights;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.HomePageHighlights.Dtos;
using ArticlesManager.Domain.HomePageHighlights.Validators;
using ArticlesManager.Domain.HomePageHighlights.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Collections;


public class HomePageHighlight : BaseEntity
{
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Article")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? ArticleId { get; private set; }
    public virtual Article Article { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Brand")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? BrandId { get; private set; }
    public virtual Brand Brand { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Collection")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? CollectionId { get; private set; }
    public virtual Collection Collection { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Name { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int? Order { get; private set; }


    public static HomePageHighlight Create(HomePageHighlightForCreationDto homePageHighlightForCreationDto)
    {
        new HomePageHighlightForCreationDtoValidator().ValidateAndThrow(homePageHighlightForCreationDto);

        var newHomePageHighlight = new HomePageHighlight();

        newHomePageHighlight.ArticleId = homePageHighlightForCreationDto.ArticleId;
        newHomePageHighlight.BrandId = homePageHighlightForCreationDto.BrandId;
        newHomePageHighlight.CollectionId = homePageHighlightForCreationDto.CollectionId;
        newHomePageHighlight.Name = homePageHighlightForCreationDto.Name;
        newHomePageHighlight.Order = homePageHighlightForCreationDto.Order;

        newHomePageHighlight.QueueDomainEvent(new HomePageHighlightCreated(){ HomePageHighlight = newHomePageHighlight });
        
        return newHomePageHighlight;
    }

    public void Update(HomePageHighlightForUpdateDto homePageHighlightForUpdateDto)
    {
        new HomePageHighlightForUpdateDtoValidator().ValidateAndThrow(homePageHighlightForUpdateDto);

        ArticleId = homePageHighlightForUpdateDto.ArticleId;
        BrandId = homePageHighlightForUpdateDto.BrandId;
        CollectionId = homePageHighlightForUpdateDto.CollectionId;
        Name = homePageHighlightForUpdateDto.Name;
        Order = homePageHighlightForUpdateDto.Order;

        QueueDomainEvent(new HomePageHighlightUpdated(){ Id = Id });
    }
    
    protected HomePageHighlight() { } // For EF + Mocking
}