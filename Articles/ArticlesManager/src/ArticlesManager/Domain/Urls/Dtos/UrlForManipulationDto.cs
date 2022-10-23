namespace ArticlesManager.Domain.Urls.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class UrlForManipulationDto 
    {
        public string UrlValue { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaName { get; set; }
    }
}