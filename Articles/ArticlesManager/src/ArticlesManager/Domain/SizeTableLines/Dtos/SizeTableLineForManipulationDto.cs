namespace ArticlesManager.Domain.SizeTableLines.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class SizeTableLineForManipulationDto 
    {
        public Guid? SizeTableId { get; set; }
        public string EU { get; set; }
        public string US { get; set; }
        public string UK { get; set; }
        public string CM { get; set; }
    }
}