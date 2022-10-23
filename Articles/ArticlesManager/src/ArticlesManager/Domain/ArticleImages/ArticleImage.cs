namespace ArticlesManager.Domain.ArticleImages;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.Domain.ArticleImages.Validators;
using ArticlesManager.Domain.ArticleImages.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Articles;


public class ArticleImage : BaseEntity
{
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Article")]
    public virtual Guid? ArticleId { get; private set; }
    public virtual Article Article { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Url { get; private set; }


    public static ArticleImage Create(ArticleImageForCreationDto articleImageForCreationDto)
    {
        new ArticleImageForCreationDtoValidator().ValidateAndThrow(articleImageForCreationDto);

        var newArticleImage = new ArticleImage();

        newArticleImage.ArticleId = articleImageForCreationDto.ArticleId;
        newArticleImage.Url = articleImageForCreationDto.Url;

        newArticleImage.QueueDomainEvent(new ArticleImageCreated(){ ArticleImage = newArticleImage });
        
        return newArticleImage;
    }

    public void Update(ArticleImageForUpdateDto articleImageForUpdateDto)
    {
        new ArticleImageForUpdateDtoValidator().ValidateAndThrow(articleImageForUpdateDto);

        ArticleId = articleImageForUpdateDto.ArticleId;
        Url = articleImageForUpdateDto.Url;

        QueueDomainEvent(new ArticleImageUpdated(){ Id = Id });
    }
    
    protected ArticleImage() { } // For EF + Mocking
}