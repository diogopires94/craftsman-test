namespace ArticlesManager.Domain.MaxiRetail
{

    public class MaxiRetailStocks
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public MaxiRetailStockResult[] Result { get; set; }
    }

    public class MaxiRetailStockResult
    {
        public string StoreShortDigits { get; set; }
        public string WarehouseCode { get; set; }
        public string ArticleId { get; set; }
        public string Reference { get; set; }
        public string Barcode { get; set; }
        public string RowCode { get; set; }
        public string ColumnCode { get; set; }
        public double StockQuantity { get; set; }
        public double ReservedQuantity { get; set; }
    }


}
