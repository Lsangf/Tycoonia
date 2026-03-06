using Microsoft.Data.SqlClient;

namespace Tycoonia.Infrastructure.SQL.Database
{
    public class DbConnectionProvider
    {
        private readonly string _connectionString;

        public DbConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
