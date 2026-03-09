using Microsoft.Data.SqlClient;

namespace Tycoonia.Infrastructure.SQL
{
    public class SQLInitializer
    {
        public static async Task<string> InitializeAsync()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TycooniaTest");

            Directory.CreateDirectory(folder);

            string dbPath = Path.Combine(folder, "TycooniaTest.mdf");

            if (!File.Exists(dbPath))
            {
                using var masterConnection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=True;");

                await masterConnection.OpenAsync();

                string createDbQuery =
                $"""
                CREATE DATABASE TycooniaTest
                ON (NAME = N'TycooniaTest',
                FILENAME = '{dbPath}')
                """;

                using var command = new SqlCommand(createDbQuery, masterConnection);
                await command.ExecuteNonQueryAsync();
            }

            string connectionString =
            $"""
            Server=(localdb)\MSSQLLocalDB;
            AttachDbFilename={dbPath};
            Integrated Security=True;
            """;

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string createTablesQuery !!=
            """
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Factories' AND xtype='U')
            CREATE TABLE Factories (
                Id INT PRIMARY KEY IDENTITY,
                FactoryType NVARCHAR(100) NOT NULL,
                Name NVARCHAR(150) NOT NULL,
                Level SMALLINT NOT NULL,
                ProductionRate INT NOT NULL,
                EnergyConsumption DECIMAL(10, 3) NOT NULL,
                WorkFlag BIT NOT NULL
            );
            """;

            using var tableCommand = new SqlCommand(createTablesQuery, connection);
            await tableCommand.ExecuteNonQueryAsync();

            return connectionString;
        }
    }
}
