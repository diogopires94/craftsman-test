namespace ArticlesManager.Domain.Promotions.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class PromotionForManipulationDto 
    {
        public string Name { get; set; }
        public string Filter { get; set; }
    }
}