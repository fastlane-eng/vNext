using FluentValidation;
using FastLane.Core.Models;

namespace FastLane.Core.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }

    public class InventoryItemValidator : AbstractValidator<InventoryItem>
    {
        public InventoryItemValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        }
    }

    public class SalesForecastValidator : AbstractValidator<SalesForecast>
    {
        public SalesForecastValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.ForecastDate).NotEmpty();
            RuleFor(x => x.ForecastedQuantity).GreaterThanOrEqualTo(0);
        }
    }

    public class PurchaseOrderValidator : AbstractValidator<PurchaseOrder>
    {
        public PurchaseOrderValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.OrderDate).NotEmpty();
        }
    }
}
