namespace FastLane.Core.Models
{
    public class Sku
    {
        public int Id { get; set; }
        public string SkuCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int QuantityOnHand { get; set; }
        public int ReorderPoint { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
