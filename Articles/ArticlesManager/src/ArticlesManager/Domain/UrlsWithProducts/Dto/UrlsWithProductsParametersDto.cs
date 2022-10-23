namespace ArticlesManager.Domain.UrlsWithProducts.Dtos
{
    using SharedKernel.Dtos;

    public class UrlsWithProductsParametersDto : BasePaginationParameters
    {
        public string Url { get; set; }
        public string Filters { get; set; }
        public string SortOrder { get; set; }

    }
}