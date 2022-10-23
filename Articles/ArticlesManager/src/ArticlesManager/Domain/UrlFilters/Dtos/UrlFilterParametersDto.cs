namespace ArticlesManager.Domain.UrlFilters.Dtos
{
    using SharedKernel.Dtos;

    public class UrlFilterParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}