using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FastLane.Core;

namespace FastLane.Data
{
    /// <summary>
    /// Repository contract for performing CRUD operations on <see cref="Product"/> entities.
    /// </summary>
    public interface IProductRepository
    {
        Task<Product?> GetAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Product product, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Dapper-based implementation of <see cref="IProductRepository"/> backed by a SQLite database.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ISqliteConnectionFactory _connectionFactory;

        public ProductRepository(ISqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Product?> GetAsync(int id, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT Id, Name, Description FROM Products WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string query = "SELECT Id, Name, Description FROM Products";
            var result = await connection.QueryAsync<Product>(query);
            return result;
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "INSERT INTO Products (Id, Name, Description) VALUES (@Id, @Name, @Description)";
            await connection.ExecuteAsync(sql, product);
        }
    }
}
