namespace ArticlesManager.Domain.Families.Dtos
{
    using SharedKernel.Dtos;

    public class FamilyParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}