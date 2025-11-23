using System;

namespace FastLane.Core.Events
{
    // Base class for domain events
    public abstract class DomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }

    // Event raised when inventory for a product falls below a threshold
    public class LowInventoryEvent : DomainEvent
    {
        public int ProductId { get; }
        public int CurrentQuantity { get; }

        public LowInventoryEvent(int productId, int currentQuantity)
        {
            ProductId = productId;
            CurrentQuantity = currentQuantity;
        }
    }

    // Event raised when a new forecast is generated for a product
    public class ForecastGeneratedEvent : DomainEvent
    {
        public int ProductId { get; }
        public int ForecastedQuantity { get; }

        public ForecastGeneratedEvent(int productId, int forecastedQuantity)
        {
            ProductId = productId;
            ForecastedQuantity = forecastedQuantity;
        }
    }

    // Event raised when a purchase order is created for a product
    public class PurchaseOrderCreatedEvent : DomainEvent
    {
        public int OrderId { get; }
        public int ProductId { get; }
        public int Quantity { get; }

        public PurchaseOrderCreatedEvent(int orderId, int productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
