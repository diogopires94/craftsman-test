namespace ArticlesManager.Domain.SizeTables.Dtos
{
    using SharedKernel.Dtos;

    public class SizeTableParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}