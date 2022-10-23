namespace ArticlesManager.Domain.UserCharts.Dtos
{
    using System.Collections.Generic;
    using System;

    public class UserChartDto 
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ArticleId { get; set; }
        public int? Quantity { get; set; }
    }
}