namespace ArticlesManager.Domain.SubFamilies.Dtos
{
    using SharedKernel.Dtos;

    public class SubFamilyParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}