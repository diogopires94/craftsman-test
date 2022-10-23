namespace ArticlesManager.Domain.Brands;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Domain.Brands.Validators;
using ArticlesManager.Domain.Brands.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Brand : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Code { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? Description { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string? ImageUrl { get; private set; }


    public static Brand Create(BrandForCreationDto brandForCreationDto)
    {
        new BrandForCreationDtoValidator().ValidateAndThrow(brandForCreationDto);

        var newBrand = new Brand();

        newBrand.Code = brandForCreationDto.Code;
        newBrand.Description = brandForCreationDto.Description;
        newBrand.ImageUrl = brandForCreationDto.ImageUrl;

        newBrand.QueueDomainEvent(new BrandCreated(){ Brand = newBrand });
        
        return newBrand;
    }

    public void Update(BrandForUpdateDto brandForUpdateDto)
    {
        new BrandForUpdateDtoValidator().ValidateAndThrow(brandForUpdateDto);

        Code = brandForUpdateDto.Code;
        Description = brandForUpdateDto.Description;
        ImageUrl = brandForUpdateDto.ImageUrl;

        QueueDomainEvent(new BrandUpdated(){ Id = Id });
    }
    
    protected Brand() { } // For EF + Mocking
}