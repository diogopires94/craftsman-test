namespace ArticlesManager.Domain.ArticleImages.Dtos
{
    using System.Collections.Generic;
    using System;

    public class ArticleImageDto 
    {
        public Guid Id { get; set; }
        public Guid? ArticleId { get; set; }
        public string? Url { get; set; }
    }
}