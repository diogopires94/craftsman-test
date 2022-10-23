namespace ArticlesManager.Domain.UserCharts;

using SharedKernel.Exceptions;
using ArticlesManager.Domain.UserCharts.Dtos;
using ArticlesManager.Domain.UserCharts.Validators;
using ArticlesManager.Domain.UserCharts.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ArticlesManager.Domain.Articles;


public class UserChart : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? UserId { get; private set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Article")]
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual Guid? ArticleId { get; private set; }
    public virtual Article Article { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int? Quantity { get; private set; }


    public static UserChart Create(UserChartForCreationDto userChartForCreationDto)
    {
        new UserChartForCreationDtoValidator().ValidateAndThrow(userChartForCreationDto);

        var newUserChart = new UserChart();

        newUserChart.UserId = userChartForCreationDto.UserId;
        newUserChart.ArticleId = userChartForCreationDto.ArticleId;
        newUserChart.Quantity = userChartForCreationDto.Quantity;

        newUserChart.QueueDomainEvent(new UserChartCreated(){ UserChart = newUserChart });
        
        return newUserChart;
    }

    public void Update(UserChartForUpdateDto userChartForUpdateDto)
    {
        new UserChartForUpdateDtoValidator().ValidateAndThrow(userChartForUpdateDto);

        UserId = userChartForUpdateDto.UserId;
        ArticleId = userChartForUpdateDto.ArticleId;
        Quantity = userChartForUpdateDto.Quantity;

        QueueDomainEvent(new UserChartUpdated(){ Id = Id });
    }
    
    protected UserChart() { } // For EF + Mocking
}