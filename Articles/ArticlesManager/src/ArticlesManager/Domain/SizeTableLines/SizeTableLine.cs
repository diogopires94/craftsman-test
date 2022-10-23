namespace ArticlesManager.Domain.SizeTableLines;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.Domain.SizeTableLines.Validators;
using ArticlesManager.Domain.SizeTableLines.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.SizeTables;


public class SizeTableLine : BaseEntity
{
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("SizeTable")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? SizeTableId { get; private set; }
    public virtual SizeTable SizeTable { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string EU { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string US { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string UK { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string CM { get; private set; }


    public static SizeTableLine Create(SizeTableLineForCreationDto sizeTableLineForCreationDto)
    {
        new SizeTableLineForCreationDtoValidator().ValidateAndThrow(sizeTableLineForCreationDto);

        var newSizeTableLine = new SizeTableLine();

        newSizeTableLine.SizeTableId = sizeTableLineForCreationDto.SizeTableId;
        newSizeTableLine.EU = sizeTableLineForCreationDto.EU;
        newSizeTableLine.US = sizeTableLineForCreationDto.US;
        newSizeTableLine.UK = sizeTableLineForCreationDto.UK;
        newSizeTableLine.CM = sizeTableLineForCreationDto.CM;

        newSizeTableLine.QueueDomainEvent(new SizeTableLineCreated(){ SizeTableLine = newSizeTableLine });
        
        return newSizeTableLine;
    }

    public void Update(SizeTableLineForUpdateDto sizeTableLineForUpdateDto)
    {
        new SizeTableLineForUpdateDtoValidator().ValidateAndThrow(sizeTableLineForUpdateDto);

        SizeTableId = sizeTableLineForUpdateDto.SizeTableId;
        EU = sizeTableLineForUpdateDto.EU;
        US = sizeTableLineForUpdateDto.US;
        UK = sizeTableLineForUpdateDto.UK;
        CM = sizeTableLineForUpdateDto.CM;

        QueueDomainEvent(new SizeTableLineUpdated(){ Id = Id });
    }
    
    protected SizeTableLine() { } // For EF + Mocking
}