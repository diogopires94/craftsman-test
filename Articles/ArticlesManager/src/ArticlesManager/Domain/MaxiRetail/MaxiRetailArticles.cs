namespace ArticlesManager.Domain.MaxiRetail
{

    public class MaxiRetailArticles
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public MaxiRetailArticlesResult[] Result { get; set; }
    }

    public class MaxiRetailArticlesResult
    {
        public MaxiRetailArticlesBarcode[] Barcodes { get; set; }
        public MaxiRetailArticlesBrand Brand { get; set; }
        public MaxiRetailArticlesCollection Collection { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public MaxiRetailArticlesFamily Family { get; set; }
        public string Generic_0 { get; set; }
        public string Generic_1 { get; set; }
        public string Id { get; set; }
        public double Price { get; set; }
        public string ROW_NUMBER { get; set; }
        public string Reference { get; set; }
        public MaxiRetailArticlesUnit Unit { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Generic_2 { get; set; }
    }

    public class MaxiRetailArticlesBrand
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class MaxiRetailArticlesCollection
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class MaxiRetailArticlesFamily
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class MaxiRetailArticlesUnit
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class MaxiRetailArticlesBarcode
    {
        public string Barcode { get; set; }
        public string ColumnCode { get; set; }
        public string ColumnDescription { get; set; }
        public double price { get; set; }
        public string RowCode { get; set; }
        public string RowDescription { get; set; }
    }

}
