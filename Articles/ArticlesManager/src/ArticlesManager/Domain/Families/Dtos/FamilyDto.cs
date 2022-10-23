namespace ArticlesManager.Domain.Families.Dtos
{
    using System.Collections.Generic;
    using System;

    public class FamilyDto 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}