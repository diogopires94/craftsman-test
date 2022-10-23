namespace ArticlesManager.Domain.Urls.Dtos
{
    using SharedKernel.Dtos;

    public class UrlParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}