namespace ArticlesManager.Domain.Families.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class FamilyForManipulationDto 
    {
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}