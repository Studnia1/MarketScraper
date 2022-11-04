using MarketScraper.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;

namespace MarketScraper.API.Repository
{
    public class ProductRespository : IProductRepository
    {
        private readonly IMongoCollection<ProductDto> _productCollection;

        public ProductRespository(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase dataBase = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _productCollection = dataBase.GetCollection<ProductDto>(mongoDBSettings.Value.CollectionName);

            var options = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<ProductDto>("{ProductUrl : 1}", options);
            _productCollection.Indexes.CreateOneAsync(indexModel);
        }

        public async Task CreateAsync(List<ProductDto> products)
        {
            await _productCollection.InsertManyAsync(products);
            return;
        }

        public async Task<List<ProductDto>> GetAsync(string tag)
        {
            var results =  await _productCollection.FindAsync(c => c.Tag == tag);
            return results.ToList();
        }
    }
}
