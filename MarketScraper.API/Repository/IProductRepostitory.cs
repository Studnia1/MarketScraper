using MarketScraper.API.Models;

namespace MarketScraper.API.Repository
{
    public interface IProductRepository
    {
        Task CreateAsync(List<ProductDto> products);
        Task<List<ProductDto>> GetAsync(string tag);
        Task<List<TitleDto>> GetTitles();
    }
}