using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FastLane.Core.Models;
using Microsoft.Data.Sqlite;

namespace FastLane.Data.Repositories
{
    /// <summary>
    /// Repository for managing forecast records.
    /// </summary>
    public class ForecastRepository
    {
        private readonly string _connectionString;

        public ForecastRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqliteConnection(_connectionString);

        /// <summary>
        /// Retrieves forecast records for a given SKU.
        /// </summary>
        public async Task<IEnumerable<Forecast>> GetForecastsForSkuAsync(int skuId)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Forecast>(
                "SELECT SkuId, ForecastDate, Quantity FROM Forecasts WHERE SkuId = @SkuId",
                new { SkuId = skuId });
        }

        /// <summary>
        /// Inserts a forecast record into the database.
        /// </summary>
        public async Task InsertAsync(Forecast forecast)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO Forecasts (SkuId, ForecastDate, Quantity) VALUES (@SkuId, @ForecastDate, @Quantity)",
                forecast);
        }
    }
}
