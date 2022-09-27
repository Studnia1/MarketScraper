using FluentValidation;
using MarketScraper.API.Models;

namespace MarketScraperAPI.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull();
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.PhotoUrl).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(p => !string.IsNullOrEmpty(p.PhotoUrl));
            RuleFor(p => p.ProductUrl).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(p => !string.IsNullOrEmpty(p.ProductUrl));
        }
    }
}
