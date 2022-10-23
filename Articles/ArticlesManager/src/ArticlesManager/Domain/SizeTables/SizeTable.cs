namespace ArticlesManager.Domain.SizeTables;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.Domain.SizeTables.Validators;
using ArticlesManager.Domain.SizeTables.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.SubFamilies;


public class SizeTable : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Name { get; private set; }

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


    public static SizeTable Create(SizeTableForCreationDto sizeTableForCreationDto)
    {
        new SizeTableForCreationDtoValidator().ValidateAndThrow(sizeTableForCreationDto);

        var newSizeTable = new SizeTable();

        newSizeTable.Name = sizeTableForCreationDto.Name;
        newSizeTable.FamilyId = sizeTableForCreationDto.FamilyId;
        newSizeTable.SubFamilyId = sizeTableForCreationDto.SubFamilyId;

        newSizeTable.QueueDomainEvent(new SizeTableCreated(){ SizeTable = newSizeTable });
        
        return newSizeTable;
    }

    public void Update(SizeTableForUpdateDto sizeTableForUpdateDto)
    {
        new SizeTableForUpdateDtoValidator().ValidateAndThrow(sizeTableForUpdateDto);

        Name = sizeTableForUpdateDto.Name;
        FamilyId = sizeTableForUpdateDto.FamilyId;
        SubFamilyId = sizeTableForUpdateDto.SubFamilyId;

        QueueDomainEvent(new SizeTableUpdated(){ Id = Id });
    }
    
    protected SizeTable() { } // For EF + Mocking
}