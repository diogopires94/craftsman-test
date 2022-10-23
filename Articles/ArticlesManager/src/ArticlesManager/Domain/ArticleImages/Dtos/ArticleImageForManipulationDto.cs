namespace ArticlesManager.Domain.ArticleImages.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class ArticleImageForManipulationDto 
    {
        public Guid? ArticleId { get; set; }
        public string? Url { get; set; }
    }
}