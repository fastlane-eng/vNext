using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FastLane.Core;

namespace FastLane.Data
{
    public interface ISalesForecastRepository
    {
        Task<SalesForecast?> GetLatestAsync(int productId);
        Task<IEnumerable<SalesForecast>> GetByProductAsync(int productId);
        Task AddAsync(SalesForecast forecast);
    }

    public class SalesForecastRepository : ISalesForecastRepository
    {
        private readonly ISqliteConnectionFactory _connectionFactory;

        public SalesForecastRepository(ISqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<SalesForecast?> GetLatestAsync(int productId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "SELECT Id, ProductId, ForecastDate, PredictedQuantity FROM SalesForecasts WHERE ProductId = @ProductId ORDER BY ForecastDate DESC LIMIT 1";
            return await connection.QuerySingleOrDefaultAsync<SalesForecast>(sql, new { ProductId = productId });
        }

        public async Task<IEnumerable<SalesForecast>> GetByProductAsync(int productId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "SELECT Id, ProductId, ForecastDate, PredictedQuantity FROM SalesForecasts WHERE ProductId = @ProductId ORDER BY ForecastDate";
            return await connection.QueryAsync<SalesForecast>(sql, new { ProductId = productId });
        }

        public async Task AddAsync(SalesForecast forecast)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "INSERT INTO SalesForecasts (ProductId, ForecastDate, PredictedQuantity) VALUES (@ProductId, @ForecastDate, @PredictedQuantity)";
            await connection.ExecuteAsync(sql, forecast);
        }
    }
}
