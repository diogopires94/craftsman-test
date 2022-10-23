namespace ArticlesManager.Domain.SizeTableLines.Dtos
{
    using System.Collections.Generic;
    using System;

    public class SizeTableLineDto 
    {
        public Guid Id { get; set; }
        public Guid? SizeTableId { get; set; }
        public string EU { get; set; }
        public string US { get; set; }
        public string UK { get; set; }
        public string CM { get; set; }
    }
}