namespace ArticlesManager.Domain.Promotions;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.Promotions.Dtos;
using ArticlesManager.Domain.Promotions.Validators;
using ArticlesManager.Domain.Promotions.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Promotion : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Name { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Filter { get; private set; }


    public static Promotion Create(PromotionForCreationDto promotionForCreationDto)
    {
        new PromotionForCreationDtoValidator().ValidateAndThrow(promotionForCreationDto);

        var newPromotion = new Promotion();

        newPromotion.Name = promotionForCreationDto.Name;
        newPromotion.Filter = promotionForCreationDto.Filter;

        newPromotion.QueueDomainEvent(new PromotionCreated(){ Promotion = newPromotion });
        
        return newPromotion;
    }

    public void Update(PromotionForUpdateDto promotionForUpdateDto)
    {
        new PromotionForUpdateDtoValidator().ValidateAndThrow(promotionForUpdateDto);

        Name = promotionForUpdateDto.Name;
        Filter = promotionForUpdateDto.Filter;

        QueueDomainEvent(new PromotionUpdated(){ Id = Id });
    }
    
    protected Promotion() { } // For EF + Mocking
}