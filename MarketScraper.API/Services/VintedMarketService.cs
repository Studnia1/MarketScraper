using HtmlAgilityPack;
using MarketScraper.API.Models;

namespace MarketScraper.API.Services
{
    public class VintedMarketService : IMarketService
    {
        private const string baseUrl = "https://www.olx.pl/d/elektronika/gry-konsole/q-słuchawki-bang/";
        private const string olxUrl = "https://www.olx.pl";


        public async Task Scrap()
        {
            var web = new HtmlWeb();
            var document = web.Load(baseUrl);
            var nodes = document.DocumentNode.SelectNodes("//div[@class='css-qfzx1y']");
            var olxData = document.QuerySelectorAll("div.css-19ucd76");
            foreach (var row in olxData)
            {
                var newData = new ProductDto();
                var priceFatherNode = row.QuerySelector("div.css-u2ayx9");
                var titleNode = row.QuerySelector("h6");
                var urlNode = row.QuerySelector("a");
                var imgNode = row.QuerySelector("img");

                if (titleNode != null || urlNode != null || priceFatherNode != null || imgNode != null)
                {
                    var url = olxUrl + urlNode.GetAttributeValue("href", null);
                    var img = imgNode.GetAttributeValue("src", null);

                    var price = priceFatherNode.LastChild.InnerText.Replace(" zł", string.Empty).Replace("do negocjacji", string.Empty);

                    Console.WriteLine("siema");

                }
            }
        }
    }
}
