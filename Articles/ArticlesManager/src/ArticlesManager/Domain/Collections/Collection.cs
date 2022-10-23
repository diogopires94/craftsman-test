namespace ArticlesManager.Domain.Collections;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Domain.Collections.Validators;
using ArticlesManager.Domain.Collections.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Collection : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Code { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Description { get; private set; }


    public static Collection Create(CollectionForCreationDto collectionForCreationDto)
    {
        new CollectionForCreationDtoValidator().ValidateAndThrow(collectionForCreationDto);

        var newCollection = new Collection();

        newCollection.Code = collectionForCreationDto.Code;
        newCollection.Description = collectionForCreationDto.Description;

        newCollection.QueueDomainEvent(new CollectionCreated(){ Collection = newCollection });
        
        return newCollection;
    }

    public void Update(CollectionForUpdateDto collectionForUpdateDto)
    {
        new CollectionForUpdateDtoValidator().ValidateAndThrow(collectionForUpdateDto);

        Code = collectionForUpdateDto.Code;
        Description = collectionForUpdateDto.Description;

        QueueDomainEvent(new CollectionUpdated(){ Id = Id });
    }
    
    protected Collection() { } // For EF + Mocking
}