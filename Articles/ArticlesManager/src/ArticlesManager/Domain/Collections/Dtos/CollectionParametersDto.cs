namespace ArticlesManager.Domain.Collections.Dtos
{
    using SharedKernel.Dtos;

    public class CollectionParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}