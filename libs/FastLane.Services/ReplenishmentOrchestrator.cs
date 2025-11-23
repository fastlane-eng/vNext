using System;
using System.Threading.Tasks;
using FastLane.Core;
using FastLane.Data;

namespace FastLane.Services
{
    public interface IReplenishmentOrchestrator
    {
        Task ReplenishAsync(int productId, int reorderPoint);
    }

    /// <summary>
    /// ReplenishmentOrchestrator coordinates inventory replenishment by generating forecasts
    /// and creating purchase orders when inventory drops below a specified reorder point.
    /// </summary>
    public class ReplenishmentOrchestrator : IReplenishmentOrchestrator
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IAnalyticsEngine _analyticsEngine;
        private readonly IEventPublisher _eventPublisher;

        public ReplenishmentOrchestrator(
            IInventoryRepository inventoryRepository,
            IProductRepository productRepository,
            IPurchaseOrderRepository purchaseOrderRepository,
            IAnalyticsEngine analyticsEngine,
            IEventPublisher eventPublisher)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
            _analyticsEngine = analyticsEngine;
            _eventPublisher = eventPublisher;
        }

        public async Task ReplenishAsync(int productId, int reorderPoint)
        {
            var inventoryItem = await _inventoryRepository.GetAsync(productId);
            if (inventoryItem == null)
            {
                throw new InvalidOperationException($"Inventory item not found for product {productId}");
            }

            if (inventoryItem.Quantity < reorderPoint)
            {
                // publish low inventory event
                await _eventPublisher.PublishAsync(new LowInventoryEvent(productId, inventoryItem.Quantity));

                // generate forecast for reorder quantity
                var forecast = await _analyticsEngine.GenerateForecastAsync(productId);

                var order = new PurchaseOrder
                {
                    ProductId = productId,
                    OrderDate = DateTime.UtcNow,
                    Quantity = forecast.Quantity
                };

                await _purchaseOrderRepository.AddAsync(order);

                await _eventPublisher.PublishAsync(new PurchaseOrderCreatedEvent(productId, order.Quantity));
            }
        }
    }
}
