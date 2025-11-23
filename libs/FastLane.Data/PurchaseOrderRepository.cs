using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FastLane.Core;

namespace FastLane.Data
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder?> GetAsync(int id);
        Task<IEnumerable<PurchaseOrder>> GetAllAsync();
        Task<int> AddAsync(PurchaseOrder order);
    }

    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly ISqliteConnectionFactory _connectionFactory;

        public PurchaseOrderRepository(ISqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PurchaseOrder?> GetAsync(int id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "SELECT Id, ProductId, Quantity, OrderDate FROM PurchaseOrders WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<PurchaseOrder>(sql, new { Id = id });
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "SELECT Id, ProductId, Quantity, OrderDate FROM PurchaseOrders";
            return await connection.QueryAsync<PurchaseOrder>(sql);
        }

        public async Task<int> AddAsync(PurchaseOrder order)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var sql = "INSERT INTO PurchaseOrders (ProductId, Quantity, OrderDate) VALUES (@ProductId, @Quantity, @OrderDate); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, order);
        }
    }
}
