namespace ArticlesManager.Domain.ArticlePromotions.Dtos
{
    using System.Collections.Generic;
    using System;

    public class ArticlePromotionDto 
    {
        public Guid Id { get; set; }
        public Guid? ArticleId { get; set; }
        public int Discount { get; set; }
        public Guid? PromotionId { get; set; }
    }
}