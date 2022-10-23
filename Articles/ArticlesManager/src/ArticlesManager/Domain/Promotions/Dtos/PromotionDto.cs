namespace ArticlesManager.Domain.Promotions.Dtos
{
    using System.Collections.Generic;
    using System;

    public class PromotionDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Filter { get; set; }
    }
}