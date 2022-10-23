namespace ArticlesManager.Domain.SubFamilies.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class SubFamilyForManipulationDto 
    {
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}