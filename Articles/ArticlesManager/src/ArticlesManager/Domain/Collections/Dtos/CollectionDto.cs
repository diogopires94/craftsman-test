namespace ArticlesManager.Domain.Collections.Dtos
{
    using System.Collections.Generic;
    using System;

    public class CollectionDto 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}