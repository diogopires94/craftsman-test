namespace ArticlesManager.Domain.UserCharts.Dtos
{
    using SharedKernel.Dtos;

    public class UserChartParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}