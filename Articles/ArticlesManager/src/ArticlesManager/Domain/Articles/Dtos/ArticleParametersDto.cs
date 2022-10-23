namespace ArticlesManager.Domain.Articles.Dtos
{
    using SharedKernel.Dtos;

    public class ArticleParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}