namespace MarketScraper.API.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ProductUrl { get; set; }
        public string? PhotoUrl { get; set; }
        public decimal? Price { get; set; }
    }
}
