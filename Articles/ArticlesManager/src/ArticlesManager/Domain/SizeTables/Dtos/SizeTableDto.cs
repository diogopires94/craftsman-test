namespace ArticlesManager.Domain.SizeTables.Dtos
{
    using System.Collections.Generic;
    using System;

    public class SizeTableDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? FamilyId { get; set; }
        public Guid? SubFamilyId { get; set; }
    }
}