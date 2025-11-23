using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FastLane.Core;

namespace FastLane.Data
{
    public interface IInventoryRepository
    {
        Task<InventoryItem?> GetAsync(int productId);
        Task<IEnumerable<InventoryItem>> GetAllAsync();
        Task AddAsync(InventoryItem item);
        Task UpdateAsync(InventoryItem item);
    }

    public class InventoryRepository : IInventoryRepository
    {
        private readonly ISqliteConnectionFactory _connectionFactory;

        public InventoryRepository(ISqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<InventoryItem?> GetAsync(int productId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "SELECT ProductId, Quantity FROM Inventory WHERE ProductId = @ProductId";
            return await connection.QuerySingleOrDefaultAsync<InventoryItem>(sql, new { ProductId = productId });
        }

        public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "SELECT ProductId, Quantity FROM Inventory";
            return await connection.QueryAsync<InventoryItem>(sql);
        }

        public async Task AddAsync(InventoryItem item)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "INSERT INTO Inventory (ProductId, Quantity) VALUES (@ProductId, @Quantity)";
            await connection.ExecuteAsync(sql, item);
        }

        public async Task UpdateAsync(InventoryItem item)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "UPDATE Inventory SET Quantity = @Quantity WHERE ProductId = @ProductId";
            await connection.ExecuteAsync(sql, item);
        }
    }
}
