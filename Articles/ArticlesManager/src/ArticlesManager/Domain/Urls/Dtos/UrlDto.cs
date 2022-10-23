namespace ArticlesManager.Domain.Urls.Dtos
{
    using System.Collections.Generic;
    using System;

    public class UrlDto 
    {
        public Guid Id { get; set; }
        public string UrlValue { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaName { get; set; }
    }
}