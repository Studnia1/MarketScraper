
using Microsoft.AspNetCore.Mvc;
using MarketScraper.API.Repository;

namespace MarketScraperAPI.Controllers
{
    [ApiController]
    public class MarketDataController : ControllerBase
    {
        private readonly IProductRepository _mongoDbService;

        //do sprawdzenia czy modelstate dziala z fluentem tutaj
        public MarketDataController(IProductRepository mongoDbService)
        {
            _mongoDbService = mongoDbService ?? throw new ArgumentNullException(nameof(mongoDbService));
        }

        [HttpGet("{tag}")]
        public async Task<IActionResult> GetProducts(string tag)
        {
            var products = await _mongoDbService.GetAsync(tag);

            if (products == null)
                return NotFound();

            return Ok(products);
        }
    }
}
