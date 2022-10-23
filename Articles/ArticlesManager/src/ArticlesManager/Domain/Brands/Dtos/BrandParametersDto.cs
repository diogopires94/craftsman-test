namespace ArticlesManager.Domain.Brands.Dtos
{
    using SharedKernel.Dtos;

    public class BrandParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}