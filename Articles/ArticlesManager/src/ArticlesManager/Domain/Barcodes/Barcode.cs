namespace ArticlesManager.Domain.Barcodes;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Barcodes.Validators;
using ArticlesManager.Domain.Barcodes.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Articles;


public class Barcode : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string BarcodeValue { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Article")]
    public virtual Guid? ArticleId { get; private set; }
    public virtual Article Article { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Size { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Size_Description { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual double? Price { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Color_Code { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Color_Description { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int? StockQuantity { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int? ReservedQuantity { get; private set; }


    public static Barcode Create(BarcodeForCreationDto barcodeForCreationDto)
    {
        new BarcodeForCreationDtoValidator().ValidateAndThrow(barcodeForCreationDto);

        var newBarcode = new Barcode();

        newBarcode.BarcodeValue = barcodeForCreationDto.BarcodeValue;
        newBarcode.ArticleId = barcodeForCreationDto.ArticleId;
        newBarcode.Size = barcodeForCreationDto.Size;
        newBarcode.Size_Description = barcodeForCreationDto.Size_Description;
        newBarcode.Price = barcodeForCreationDto.Price;
        newBarcode.Color_Code = barcodeForCreationDto.Color_Code;
        newBarcode.Color_Description = barcodeForCreationDto.Color_Description;
        newBarcode.StockQuantity = barcodeForCreationDto.StockQuantity;
        newBarcode.ReservedQuantity = barcodeForCreationDto.ReservedQuantity;

        newBarcode.QueueDomainEvent(new BarcodeCreated(){ Barcode = newBarcode });
        
        return newBarcode;
    }

    public void Update(BarcodeForUpdateDto barcodeForUpdateDto)
    {
        new BarcodeForUpdateDtoValidator().ValidateAndThrow(barcodeForUpdateDto);

        BarcodeValue = barcodeForUpdateDto.BarcodeValue;
        ArticleId = barcodeForUpdateDto.ArticleId;
        Size = barcodeForUpdateDto.Size;
        Size_Description = barcodeForUpdateDto.Size_Description;
        Price = barcodeForUpdateDto.Price;
        Color_Code = barcodeForUpdateDto.Color_Code;
        Color_Description = barcodeForUpdateDto.Color_Description;
        StockQuantity = barcodeForUpdateDto.StockQuantity;
        ReservedQuantity = barcodeForUpdateDto.ReservedQuantity;

        QueueDomainEvent(new BarcodeUpdated(){ Id = Id });
    }
    
    protected Barcode() { } // For EF + Mocking
}