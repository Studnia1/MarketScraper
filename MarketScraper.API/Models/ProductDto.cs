using MongoDB.Bson.Serialization.Attributes;

namespace MarketScraper.API.Models
{
    public class ProductDto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ProductUrl { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Tag { get; set; }
        public decimal? Price { get; set; }
    }
}
