namespace ArticlesManager.Domain.Barcodes.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class BarcodeForManipulationDto 
    {
        public string BarcodeValue { get; set; }
        public Guid? ArticleId { get; set; }
        public string? Size { get; set; }
        public string? Size_Description { get; set; }
        public double? Price { get; set; }
        public string? Color_Code { get; set; }
        public string? Color_Description { get; set; }
        public int? StockQuantity { get; set; }
        public int? ReservedQuantity { get; set; }
    }
}