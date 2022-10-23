namespace ArticlesManager.Domain.UrlFilters.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class UrlFilterForManipulationDto 
    {
        public Guid? UrlId { get; set; }
        public Guid? FamilyId { get; set; }
        public Guid? SubFamilyId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CollectionId { get; set; }
    }
}