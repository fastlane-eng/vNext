using System;

namespace FastLane.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class InventoryItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class SalesForecast
    {
        public int ProductId { get; set; }
        public DateTime ForecastDate { get; set; }
        public int ForecastedQuantity { get; set; }
    }

    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
