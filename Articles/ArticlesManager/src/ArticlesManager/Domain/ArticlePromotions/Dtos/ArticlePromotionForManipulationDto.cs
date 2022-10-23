namespace ArticlesManager.Domain.ArticlePromotions.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class ArticlePromotionForManipulationDto 
    {
        public Guid? ArticleId { get; set; }
        public int Discount { get; set; }
        public Guid? PromotionId { get; set; }
    }
}