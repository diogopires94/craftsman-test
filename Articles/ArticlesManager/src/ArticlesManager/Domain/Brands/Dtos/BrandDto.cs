namespace ArticlesManager.Domain.Brands.Dtos
{
    using System.Collections.Generic;
    using System;

    public class BrandDto 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}