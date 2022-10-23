namespace ArticlesManager.Domain.Articles;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.Articles.Validators;
using ArticlesManager.Domain.Articles.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Brands;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.Collections;


public class Article : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string InternalReference { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string SKU { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Description { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual double? Price { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual double? PriceWithPromotion { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Brand")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? BrandId { get; private set; }
    public virtual Brand Brand { get; private set; }

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
    [ForeignKey("Collection")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? CollectionId { get; private set; }
    public virtual Collection Collection { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Generic1 { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? RowNumber { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? MainArticleImageUrl { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Url { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? MetaName { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? MetaDescription { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool IsLowStock { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool IsOutOfStock { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool IsPublished { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual bool IsOutlet { get; private set; }


    public static Article Create(ArticleForCreationDto articleForCreationDto)
    {
        new ArticleForCreationDtoValidator().ValidateAndThrow(articleForCreationDto);

        var newArticle = new Article();

        newArticle.InternalReference = articleForCreationDto.InternalReference;
        newArticle.SKU = articleForCreationDto.SKU;
        newArticle.Description = articleForCreationDto.Description;
        newArticle.Price = articleForCreationDto.Price;
        newArticle.PriceWithPromotion = articleForCreationDto.PriceWithPromotion;
        newArticle.BrandId = articleForCreationDto.BrandId;
        newArticle.FamilyId = articleForCreationDto.FamilyId;
        newArticle.SubFamilyId = articleForCreationDto.SubFamilyId;
        newArticle.CollectionId = articleForCreationDto.CollectionId;
        newArticle.Generic1 = articleForCreationDto.Generic1;
        newArticle.RowNumber = articleForCreationDto.RowNumber;
        newArticle.MainArticleImageUrl = articleForCreationDto.MainArticleImageUrl;
        newArticle.Url = articleForCreationDto.Url;
        newArticle.MetaName = articleForCreationDto.MetaName;
        newArticle.MetaDescription = articleForCreationDto.MetaDescription;
        newArticle.IsLowStock = articleForCreationDto.IsLowStock;
        newArticle.IsOutOfStock = articleForCreationDto.IsOutOfStock;
        newArticle.IsPublished = articleForCreationDto.IsPublished;
        newArticle.IsOutlet = articleForCreationDto.IsOutlet;

        newArticle.QueueDomainEvent(new ArticleCreated(){ Article = newArticle });
        
        return newArticle;
    }

    public void Update(ArticleForUpdateDto articleForUpdateDto)
    {
        new ArticleForUpdateDtoValidator().ValidateAndThrow(articleForUpdateDto);

        InternalReference = articleForUpdateDto.InternalReference;
        SKU = articleForUpdateDto.SKU;
        Description = articleForUpdateDto.Description;
        Price = articleForUpdateDto.Price;
        PriceWithPromotion = articleForUpdateDto.PriceWithPromotion;
        BrandId = articleForUpdateDto.BrandId;
        FamilyId = articleForUpdateDto.FamilyId;
        SubFamilyId = articleForUpdateDto.SubFamilyId;
        CollectionId = articleForUpdateDto.CollectionId;
        Generic1 = articleForUpdateDto.Generic1;
        RowNumber = articleForUpdateDto.RowNumber;
        MainArticleImageUrl = articleForUpdateDto.MainArticleImageUrl;
        Url = articleForUpdateDto.Url;
        MetaName = articleForUpdateDto.MetaName;
        MetaDescription = articleForUpdateDto.MetaDescription;
        IsLowStock = articleForUpdateDto.IsLowStock;
        IsOutOfStock = articleForUpdateDto.IsOutOfStock;
        IsPublished = articleForUpdateDto.IsPublished;
        IsOutlet = articleForUpdateDto.IsOutlet;

        QueueDomainEvent(new ArticleUpdated(){ Id = Id });
    }
    
    protected Article() { } // For EF + Mocking
}