namespace ArticlesManager.Domain.HomePageHighlights.Dtos
{
    using SharedKernel.Dtos;

    public class HomePageHighlightParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}