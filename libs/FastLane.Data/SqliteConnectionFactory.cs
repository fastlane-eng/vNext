using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace FastLane.Data
{
    /// <summary>
    /// Factory for creating SQLite database connections. Uses Microsoft.Data.Sqlite to provide lightweight connections.
    /// </summary>
    public interface ISqliteConnectionFactory
    {
        /// <summary>
        /// Creates and opens a new SQLite database connection.
        /// </summary>
        /// <returns>An open IDbConnection instance.</returns>
        IDbConnection CreateConnection();
    }

    /// <summary>
    /// Default implementation of <see cref="ISqliteConnectionFactory"/> that opens SQLite connections with a provided connection string.
    /// </summary>
    public class SqliteConnectionFactory : ISqliteConnectionFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">The SQLite connection string.</param>
        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <inheritdoc/>
        public IDbConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
