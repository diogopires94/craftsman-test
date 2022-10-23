namespace ArticlesManager.Domain.SubFamilies;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Domain.SubFamilies.Validators;
using ArticlesManager.Domain.SubFamilies.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class SubFamily : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Code { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Description { get; private set; }


    public static SubFamily Create(SubFamilyForCreationDto subFamilyForCreationDto)
    {
        new SubFamilyForCreationDtoValidator().ValidateAndThrow(subFamilyForCreationDto);

        var newSubFamily = new SubFamily();

        newSubFamily.Code = subFamilyForCreationDto.Code;
        newSubFamily.Description = subFamilyForCreationDto.Description;

        newSubFamily.QueueDomainEvent(new SubFamilyCreated(){ SubFamily = newSubFamily });
        
        return newSubFamily;
    }

    public void Update(SubFamilyForUpdateDto subFamilyForUpdateDto)
    {
        new SubFamilyForUpdateDtoValidator().ValidateAndThrow(subFamilyForUpdateDto);

        Code = subFamilyForUpdateDto.Code;
        Description = subFamilyForUpdateDto.Description;

        QueueDomainEvent(new SubFamilyUpdated(){ Id = Id });
    }
    
    protected SubFamily() { } // For EF + Mocking
}