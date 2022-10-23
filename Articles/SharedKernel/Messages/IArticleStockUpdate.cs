namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface IIArticleStockUpdate
    {
        public string StoreCode { get; set; }

public string Barcode { get; set; }

public string Reference { get; set; }

public int Stock { get; set; }

public string WarehouseCode { get; set; }

public int ReservedQuantity { get; set; }
    }

    public class IArticleStockUpdate : IIArticleStockUpdate
    {
        public string StoreCode { get; set; }

public string Barcode { get; set; }

public string Reference { get; set; }

public int Stock { get; set; }

public string WarehouseCode { get; set; }

public int ReservedQuantity { get; set; }
    }
}