using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastLane.Core.Interfaces;
using FastLane.Core.Models;
using FastLane.Data.Repositories;

namespace FastLane.Services
{
    /// <summary>
    /// Provides forecast-related operations using the forecast repository.
    /// </summary>
    public class ForecastService : IForecastService
    {
        private readonly ForecastRepository _repository;

        public ForecastService(ForecastRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Forecast>> GetForecastsForSkuAsync(int skuId)
        {
            return _repository.GetForecastsForSkuAsync(skuId);
        }

        public Task CreateForecastAsync(Forecast forecast)
        {
            return _repository.InsertForecastAsync(forecast);
        }
    }
}
