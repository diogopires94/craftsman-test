namespace ArticlesManager.Domain.Collections.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class CollectionForManipulationDto 
    {
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}