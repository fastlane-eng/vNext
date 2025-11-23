using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastLane.Core.Models;

namespace FastLane.Core.Interfaces
{
    /// <summary>
    /// Provides methods to retrieve and generate forecasts for SKUs.
    /// </summary>
    public interface IForecastService
    {
        /// <summary>
        /// Retrieves forecast entries for a given SKU.
        /// </summary>
        /// <param name="skuId">The identifier of the SKU.</param>
        /// <returns>A list of forecasts.</returns>
        Task<IEnumerable<Forecast>> GetForecastsForSkuAsync(int skuId);

        /// <summary>
        /// Generates new forecasts for the specified SKU over a date range.
        /// </summary>
        /// <param name="skuId">The identifier of the SKU.</param>
        /// <param name="startDate">The start date of the forecast horizon.</param>
        /// <param name="endDate">The end date of the forecast horizon.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task GenerateForecastAsync(int skuId, DateTime startDate, DateTime endDate);
    }
}
