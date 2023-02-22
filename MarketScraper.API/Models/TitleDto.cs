using MongoDB.Bson.Serialization.Attributes;

namespace MarketScraper.API.Models
{
    public class TitleDto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public decimal? PriceLimit { get; set; }
        public bool? Alert { get; set; }
    }
}
