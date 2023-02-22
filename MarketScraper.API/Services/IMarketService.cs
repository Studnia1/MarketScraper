using MarketScraper.API.Models;

namespace MarketScraper.API.Services
{
    public interface IMarketService
    {
        Task IterateScrapThroughAllGames();
        Task Scrap(TitleDto title);
    }
}