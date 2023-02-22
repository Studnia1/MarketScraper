using HtmlAgilityPack;
using MarketScraper.API.Models;
using MarketScraper.API.Repository;

namespace MarketScraper.API.Services
{
	public class OlxMarketService : IMarketService
    {
        private const string olxUrl = "https://www.olx.pl";

        IProductRepository mongoDb;

        public OlxMarketService(IProductRepository mongoDb)
        {
            this.mongoDb = mongoDb;
        }
        
        public async Task IterateScrapThroughAllGames()
        {
            var titlesList = await mongoDb.GetTitles();
            foreach(var title in titlesList)
            {
                await Scrap(title);
            }    
        }

        public async Task Scrap(TitleDto title)
        {
            var web = new HtmlWeb();
            var document = web.Load($"https://www.olx.pl/d/elektronika/gry-konsole/gry/q-{title.Title}/");
            var nodes = document.DocumentNode.SelectNodes("//div[@class='css-1sw7q4x']");
            var olxData = document.QuerySelectorAll("div.css-19ucd76");
            var productsList = new List<ProductDto>();
            foreach (var row in nodes)
            {
                var newData = new ProductDto();
                var priceFatherNode = row.QuerySelector("div.css-u2ayx9");
                var titleNode = row.QuerySelector("h6");
                var urlNode = row.QuerySelector("a");
                var imgNode = row.QuerySelector("img");

                if (titleNode != null && urlNode != null && priceFatherNode != null && imgNode != null )
                {
                    var price = decimal.Parse(priceFatherNode.LastChild.InnerText.Replace(" zł", string.Empty).Replace("do negocjacji", string.Empty));
                    if (price < title.PriceLimit)
                    {
                        newData.ProductUrl = olxUrl + urlNode.GetAttributeValue("href", null);
                        newData.PhotoUrl = imgNode.GetAttributeValue("src", null);
                        newData.Name = titleNode.InnerText;
                        newData.Price = price;
                        productsList.Add(newData);
                    }
                }
            }
            await mongoDb.CreateAsync(productsList);
        }
    }
}
