namespace ArticlesManager.Domain.SubFamilies.Dtos
{
    using System.Collections.Generic;
    using System;

    public class SubFamilyDto 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}