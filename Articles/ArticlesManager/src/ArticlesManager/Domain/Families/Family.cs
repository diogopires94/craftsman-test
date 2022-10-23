namespace ArticlesManager.Domain.Families;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Domain.Families.Validators;
using ArticlesManager.Domain.Families.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Family : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Code { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Description { get; private set; }


    public static Family Create(FamilyForCreationDto familyForCreationDto)
    {
        new FamilyForCreationDtoValidator().ValidateAndThrow(familyForCreationDto);

        var newFamily = new Family();

        newFamily.Code = familyForCreationDto.Code;
        newFamily.Description = familyForCreationDto.Description;

        newFamily.QueueDomainEvent(new FamilyCreated(){ Family = newFamily });
        
        return newFamily;
    }

    public void Update(FamilyForUpdateDto familyForUpdateDto)
    {
        new FamilyForUpdateDtoValidator().ValidateAndThrow(familyForUpdateDto);

        Code = familyForUpdateDto.Code;
        Description = familyForUpdateDto.Description;

        QueueDomainEvent(new FamilyUpdated(){ Id = Id });
    }
    
    protected Family() { } // For EF + Mocking
}