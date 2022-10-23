namespace ArticlesManager.Domain.UrlsWithFilters.Dto
{

    public class UrlsWithFiltersDto
    {
        public List<Brand> Brands { get; set; }
        public List<Size> Sizes { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        //public List<Family> Family { get; set; }
        public List<SubFamily> SubFamily { get; set; }

        public bool IsOutletAvailable { get; set; }
        
    }
    public class Size
    {
        public string US { get; set; }

    }

    public class Brand
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }

    //public class Family
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public List<SubFamily> SubFamilies { get; set; }

    //}

    public class SubFamily
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}
