namespace ArticlesManager.Domain.UrlFilters;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.Domain.UrlFilters.Validators;
using ArticlesManager.Domain.UrlFilters.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Urls;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Collections;


public class UrlFilter : BaseEntity
{
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Url")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? UrlId { get; private set; }
    public virtual Url Url { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Family")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? FamilyId { get; private set; }
    public virtual Family Family { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("SubFamily")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? SubFamilyId { get; private set; }
    public virtual SubFamily SubFamily { get; private set; }

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


    public static UrlFilter Create(UrlFilterForCreationDto urlFilterForCreationDto)
    {
        new UrlFilterForCreationDtoValidator().ValidateAndThrow(urlFilterForCreationDto);

        var newUrlFilter = new UrlFilter();

        newUrlFilter.UrlId = urlFilterForCreationDto.UrlId;
        newUrlFilter.FamilyId = urlFilterForCreationDto.FamilyId;
        newUrlFilter.SubFamilyId = urlFilterForCreationDto.SubFamilyId;
        newUrlFilter.BrandId = urlFilterForCreationDto.BrandId;
        newUrlFilter.CollectionId = urlFilterForCreationDto.CollectionId;

        newUrlFilter.QueueDomainEvent(new UrlFilterCreated(){ UrlFilter = newUrlFilter });
        
        return newUrlFilter;
    }

    public void Update(UrlFilterForUpdateDto urlFilterForUpdateDto)
    {
        new UrlFilterForUpdateDtoValidator().ValidateAndThrow(urlFilterForUpdateDto);

        UrlId = urlFilterForUpdateDto.UrlId;
        FamilyId = urlFilterForUpdateDto.FamilyId;
        SubFamilyId = urlFilterForUpdateDto.SubFamilyId;
        BrandId = urlFilterForUpdateDto.BrandId;
        CollectionId = urlFilterForUpdateDto.CollectionId;

        QueueDomainEvent(new UrlFilterUpdated(){ Id = Id });
    }
    
    protected UrlFilter() { } // For EF + Mocking
}