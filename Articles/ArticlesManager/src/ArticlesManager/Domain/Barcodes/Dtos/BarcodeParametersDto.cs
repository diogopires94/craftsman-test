namespace ArticlesManager.Domain.Barcodes.Dtos
{
    using SharedKernel.Dtos;

    public class BarcodeParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}