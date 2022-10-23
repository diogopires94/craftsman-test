namespace ArticlesManager.Domain.SizeTableLines.Dtos
{
    using SharedKernel.Dtos;

    public class SizeTableLineParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}