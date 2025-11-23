using System;
using System.Collections.Generic;

namespace FastLane.Core.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SupplierId { get; set; }
        public List<PurchaseOrderLine> Lines { get; set; } = new();
    }

    public class PurchaseOrderLine
    {
        public int SkuId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
