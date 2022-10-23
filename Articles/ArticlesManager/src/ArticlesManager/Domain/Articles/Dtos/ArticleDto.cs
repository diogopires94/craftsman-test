namespace ArticlesManager.Domain.Articles.Dtos
{
    using System.Collections.Generic;
    using System;

    public class ArticleDto 
    {
        public Guid Id { get; set; }
        public string InternalReference { get; set; }
        public string SKU { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public double? PriceWithPromotion { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? FamilyId { get; set; }
        public Guid? SubFamilyId { get; set; }
        public Guid? CollectionId { get; set; }
        public string? Generic1 { get; set; }
        public string? RowNumber { get; set; }
        public string? MainArticleImageUrl { get; set; }
        public string? Url { get; set; }
        public string? MetaName { get; set; }
        public string? MetaDescription { get; set; }
        public bool IsLowStock { get; set; }
        public bool IsOutOfStock { get; set; }
        public bool IsPublished { get; set; }
        public bool IsOutlet { get; set; }
    }
}