namespace ArticlesManager.Domain.ArticlePromotions;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.ArticlePromotions.Dtos;
using ArticlesManager.Domain.ArticlePromotions.Validators;
using ArticlesManager.Domain.ArticlePromotions.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Promotions;


public class ArticlePromotion : BaseEntity
{
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Article")]
    public virtual Guid? ArticleId { get; private set; }
    public virtual Article Article { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int Discount { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Promotion")]
    public virtual Guid? PromotionId { get; private set; }
    public virtual Promotion Promotion { get; private set; }


    public static ArticlePromotion Create(ArticlePromotionForCreationDto articlePromotionForCreationDto)
    {
        new ArticlePromotionForCreationDtoValidator().ValidateAndThrow(articlePromotionForCreationDto);

        var newArticlePromotion = new ArticlePromotion();

        newArticlePromotion.ArticleId = articlePromotionForCreationDto.ArticleId;
        newArticlePromotion.Discount = articlePromotionForCreationDto.Discount;
        newArticlePromotion.PromotionId = articlePromotionForCreationDto.PromotionId;

        newArticlePromotion.QueueDomainEvent(new ArticlePromotionCreated(){ ArticlePromotion = newArticlePromotion });
        
        return newArticlePromotion;
    }

    public void Update(ArticlePromotionForUpdateDto articlePromotionForUpdateDto)
    {
        new ArticlePromotionForUpdateDtoValidator().ValidateAndThrow(articlePromotionForUpdateDto);

        ArticleId = articlePromotionForUpdateDto.ArticleId;
        Discount = articlePromotionForUpdateDto.Discount;
        PromotionId = articlePromotionForUpdateDto.PromotionId;

        QueueDomainEvent(new ArticlePromotionUpdated(){ Id = Id });
    }
    
    protected ArticlePromotion() { } // For EF + Mocking
}