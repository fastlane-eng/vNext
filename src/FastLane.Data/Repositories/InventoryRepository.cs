using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FastLane.Core.Models;
using Microsoft.Data.Sqlite;

namespace FastLane.Data.Repositories
{
    /// <summary>
    /// Repository for accessing and modifying SKU inventory records.
    /// </summary>
    public class InventoryRepository
    {
        private readonly string _connectionString;

        public InventoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqliteConnection(_connectionString);

        /// <summary>
        /// Retrieves all SKU records from the database.
        /// </summary>
        public async Task<IEnumerable<Sku>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Sku>(
                "SELECT Id, SkuCode, Description, QuantityOnHand, ReorderPoint, UnitPrice FROM Skus");
        }

        /// <summary>
        /// Retrieves a SKU record by its identifier.
        /// </summary>
        public async Task<Sku?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Sku>(
                "SELECT Id, SkuCode, Description, QuantityOnHand, ReorderPoint, UnitPrice FROM Skus WHERE Id = @Id",
                new { Id = id });
        }

        /// <summary>
        /// Updates the quantity on hand for a given SKU.
        /// </summary>
        public async Task UpdateQuantityAsync(int skuId, int delta)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(
                "UPDATE Skus SET QuantityOnHand = QuantityOnHand + @Delta WHERE Id = @Id",
                new { Delta = delta, Id = skuId });
        }
    }
}
