using Sieve.Attributes;

namespace ArticlesManager.Domain.UrlsWithProducts.Dto
{
    public class UrlsWithProductsDto
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string article_sku { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string article_description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public double? article_price_with_promotion { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string article_main_article_image_url { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string article_meta_name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string article_meta_description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool article_is_low_stock { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool article_is_outlet { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string brand_description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string family_description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string sub_family_description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string collection_description { get; set; }
    }


}
