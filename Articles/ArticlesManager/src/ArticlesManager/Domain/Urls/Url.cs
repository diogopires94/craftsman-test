namespace ArticlesManager.Domain.Urls;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Domain.Urls.Validators;
using ArticlesManager.Domain.Urls.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Url : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string UrlValue { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string PageTitle { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string MetaDescription { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string MetaName { get; private set; }


    public static Url Create(UrlForCreationDto urlForCreationDto)
    {
        new UrlForCreationDtoValidator().ValidateAndThrow(urlForCreationDto);

        var newUrl = new Url();

        newUrl.UrlValue = urlForCreationDto.UrlValue;
        newUrl.PageTitle = urlForCreationDto.PageTitle;
        newUrl.MetaDescription = urlForCreationDto.MetaDescription;
        newUrl.MetaName = urlForCreationDto.MetaName;

        newUrl.QueueDomainEvent(new UrlCreated(){ Url = newUrl });
        
        return newUrl;
    }

    public void Update(UrlForUpdateDto urlForUpdateDto)
    {
        new UrlForUpdateDtoValidator().ValidateAndThrow(urlForUpdateDto);

        UrlValue = urlForUpdateDto.UrlValue;
        PageTitle = urlForUpdateDto.PageTitle;
        MetaDescription = urlForUpdateDto.MetaDescription;
        MetaName = urlForUpdateDto.MetaName;

        QueueDomainEvent(new UrlUpdated(){ Id = Id });
    }
    
    protected Url() { } // For EF + Mocking
}