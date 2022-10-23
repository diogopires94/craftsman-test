namespace ArticlesManager.Domain.HomePageHighlights.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class HomePageHighlightForManipulationDto 
    {
        public Guid? ArticleId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CollectionId { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
    }
}