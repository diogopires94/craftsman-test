namespace ArticlesManager.Domain.SizeTables.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class SizeTableForManipulationDto 
    {
        public string Name { get; set; }
        public Guid? FamilyId { get; set; }
        public Guid? SubFamilyId { get; set; }
    }
}