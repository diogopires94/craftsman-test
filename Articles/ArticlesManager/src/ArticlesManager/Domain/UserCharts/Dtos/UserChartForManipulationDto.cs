namespace ArticlesManager.Domain.UserCharts.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class UserChartForManipulationDto 
    {
        public Guid? UserId { get; set; }
        public Guid? ArticleId { get; set; }
        public int? Quantity { get; set; }
    }
}