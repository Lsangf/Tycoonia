using Microsoft.Data.SqlClient;

namespace Tycoonia.Infrastructure.SQL
{
    public class SQLInitializer
    {
        public static async Task<string> InitializeAsync()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TycooniaTest");
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

            string createTableFactories =
            """
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Factories' AND xtype='U')
            CREATE TABLE Factories (
                Id INT PRIMARY KEY IDENTITY,
                Name NVARCHAR(150) NOT NULL,
                Level SMALLINT NOT NULL,
                ProductionRate INT NOT NULL,
                EnergyConsumption DECIMAL(10, 3) NOT NULL,
                ProductionTime DECIMAL(10, 3) NOT NULL,
                ProductionTimePerIteration DECIMAL(10, 3) NOT NULL,
                WorkFlag BIT NOT NULL
            );
            """;// CTF

            string createTableFactoriesTypes =
            """
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FactoriesTypes' AND xtype='U')
            CREATE TABLE FactoriesTypes (
                Id INT PRIMARY KEY IDENTITY,
                FactoryId INT NOT NULL,
                Type NVARCHAR(100) NOT NULL,
                FOREIGN KEY(FactoryId) REFERENCES Factories(Id) ON DELETE CASCADE
            );
            """;// CTFT

            string createTableFactoriesRecipeUpgradeList =
            """
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FactoriesRecipeUpgradeList' AND xtype='U')
            CREATE TABLE FactoriesRecipeUpgradeList (
                Id INT PRIMARY KEY IDENTITY,
                FactoryId INT NOT NULL,
                Name NVARCHAR(100) NOT NULL,
                Amount BIGINT NOT NULL,
                FOREIGN KEY(FactoryId) REFERENCES Factories(Id) ON DELETE CASCADE
            );
            """;// CTFRUL

            string createTableFactoriesResourceBuffer =
            """
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FactoriesResourceBuffer' AND xtype='U')
            CREATE TABLE FactoriesResourceBuffer (
                Id INT PRIMARY KEY IDENTITY,
                FactoryId INT NOT NULL,
                Name NVARCHAR(100) NOT NULL,
                FOREIGN KEY(FactoryId) REFERENCES Factories(Id) ON DELETE CASCADE
            );
            """;// CTFRB

            string createTableFRBStorageResourcesBase =
            """
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FRBStorageResourcesBase' AND xtype='U')
            CREATE TABLE FRBStorageResourcesBase (
                Id INT PRIMARY KEY IDENTITY,
                ResourceBufferId INT NOT NULL,
                CurrentQuantity BIGINT NOT NULL,
                MaxCapacity BIGINT NOT NULL,
                UpgradeCost BIGINT NOT NULL,
                Level SMALLINT NOT NULL,
                Price INT NOT NULL,
                FOREIGN KEY(ResourceBufferId) REFERENCES FactoriesResourceBuffer(Id) ON DELETE CASCADE
            );
            """;// CTFRBSRB

            string createTableFactoriesProductionItemList =
            """
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FactoriesProductionItemList' AND xtype='U')
            CREATE TABLE FactoriesProductionItemList (
                Id INT PRIMARY KEY IDENTITY,
                FactoryId INT NOT NULL,
                Name NVARCHAR(100) NOT NULL,
                Amount INT NOT NULL,
                FOREIGN KEY(FactoryId) REFERENCES Factories(Id) ON DELETE CASCADE
            );
            """;// CTFPIL

            using var tableCommandCTF = new SqlCommand(createTableFactories, connection);
            using var tableCommandCTFT = new SqlCommand(createTableFactoriesTypes, connection);
            using var tableCommandCTFRUL = new SqlCommand(createTableFactoriesRecipeUpgradeList, connection);
            using var tableCommandCTFRB = new SqlCommand(createTableFactoriesResourceBuffer, connection);
            using var tableCommandCTFRBSRB = new SqlCommand(createTableFRBStorageResourcesBase, connection);
            using var tableCommandCTFPIL = new SqlCommand(createTableFactoriesProductionItemList, connection);
            await tableCommandCTF.ExecuteNonQueryAsync();
            await tableCommandCTFT.ExecuteNonQueryAsync();
            await tableCommandCTFRUL.ExecuteNonQueryAsync();
            await tableCommandCTFRB.ExecuteNonQueryAsync();
            await tableCommandCTFRBSRB.ExecuteNonQueryAsync();
            await tableCommandCTFPIL.ExecuteNonQueryAsync();

            return connectionString;
        }
    }
}
