using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using FastLane.Core.Models;

namespace FastLane.Data.Repositories
{
    /// <summary>
    /// Repository for working with purchase orders using Dapper and SQLite.
    /// </summary>
    public class PurchaseOrderRepository
    {
        private readonly Func<IDbConnection> _connectionFactory;

        public PurchaseOrderRepository(Func<IDbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Creates a new purchase order and returns its generated ID.
        /// Also inserts purchase order line items.
        /// </summary>
        public async Task<int> CreatePurchaseOrderAsync(PurchaseOrder po)
        {
            using var connection = _connectionFactory();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var insertPoSql = "INSERT INTO PurchaseOrders (CreatedDate, SupplierId) VALUES (@CreatedDate, @SupplierId); SELECT last_insert_rowid();";
            var poId = await connection.ExecuteScalarAsync<int>(insertPoSql, new { po.CreatedDate, po.SupplierId }, transaction);

            foreach (var line in po.Lines)
            {
                var insertLineSql = "INSERT INTO PurchaseOrderLines (PurchaseOrderId, SkuId, Quantity, UnitPrice) VALUES (@PurchaseOrderId, @SkuId, @Quantity, @UnitPrice);";
                await connection.ExecuteAsync(insertLineSql, new { PurchaseOrderId = poId, line.SkuId, line.Quantity, line.UnitPrice }, transaction);
            }

            transaction.Commit();
            return poId;
        }

        /// <summary>
        /// Retrieves a purchase order with its line items.
        /// </summary>
        public async Task<PurchaseOrder> GetPurchaseOrderByIdAsync(int id)
        {
            using var connection = _connectionFactory();

            var po = await connection.QuerySingleOrDefaultAsync<PurchaseOrder>(
                "SELECT Id, CreatedDate, SupplierId FROM PurchaseOrders WHERE Id = @Id;",
                new { Id = id });

            if (po == null)
            {
                return null;
            }

            var lines = await connection.QueryAsync<PurchaseOrder.PurchaseOrderLine>(
                "SELECT SkuId, Quantity, UnitPrice FROM PurchaseOrderLines WHERE PurchaseOrderId = @Id;",
                new { Id = id });

            po.Lines = lines.AsList();
            return po;
        }
    }
}
