namespace ArticlesManager.Domain.ArticleImages.Dtos
{
    using SharedKernel.Dtos;

    public class ArticleImageParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}