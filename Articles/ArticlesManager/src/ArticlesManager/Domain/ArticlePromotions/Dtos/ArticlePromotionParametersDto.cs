namespace ArticlesManager.Domain.ArticlePromotions.Dtos
{
    using SharedKernel.Dtos;

    public class ArticlePromotionParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}