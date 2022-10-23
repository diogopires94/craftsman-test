namespace ArticlesManager.Domain.HomePageHighlights.Dtos
{
    using System.Collections.Generic;
    using System;

    public class HomePageHighlightDto 
    {
        public Guid Id { get; set; }
        public Guid? ArticleId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CollectionId { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
    }
}