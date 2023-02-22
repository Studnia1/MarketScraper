namespace MarketScraper.API.Models
{
    public class MongoDbSettings
    {
        public string? ConnectionURI { get; set; }
        public string? DatabaseName { get; set; }
        public string? CollectionProductsName { get; set; }
        public string? CollectionTitlesName { get; set; }
    }
}
