using System;
using System.Threading.Tasks;
using FastLane.Core;
using FastLane.Data;

namespace FastLane.Services
{
    public interface IAnalyticsEngine
    {
        Task<SalesForecast> GenerateForecastAsync(int productId);
    }

    /// <summary>
    /// AnalyticsEngine generates sales forecasts based on inventory data.
    /// This implementation uses simple heuristics for demonstration and can be replaced with more advanced analytics.
    /// </summary>
    public class AnalyticsEngine : IAnalyticsEngine
    {
        private readonly ISalesForecastRepository _forecastRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IEventPublisher _eventPublisher;

        public AnalyticsEngine(
            ISalesForecastRepository forecastRepository,
            IInventoryRepository inventoryRepository,
            IEventPublisher eventPublisher)
        {
            _forecastRepository = forecastRepository;
            _inventoryRepository = inventoryRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<SalesForecast> GenerateForecastAsync(int productId)
        {
            var inventoryItem = await _inventoryRepository.GetAsync(productId);
            int currentQuantity = inventoryItem?.Quantity ?? 0;
            // For demonstration, forecast next period's demand as 50% more than current quantity.
            int predictedQuantity = (int)Math.Ceiling(currentQuantity * 1.5);

            var forecast = new SalesForecast
            {
                ProductId = productId,
                ForecastDate = DateTime.UtcNow,
                Quantity = predictedQuantity
            };

            await _forecastRepository.AddAsync(forecast);
            await _eventPublisher.PublishAsync(new ForecastGeneratedEvent(productId, predictedQuantity));

            return forecast;
        }
    }
}
