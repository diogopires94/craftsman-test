namespace ArticlesManager.Domain.Promotions.Dtos
{
    using SharedKernel.Dtos;

    public class PromotionParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}