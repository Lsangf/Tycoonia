using Microsoft.Data.SqlClient;
using System.Data;
using Tycoonia.Application.Interfaces;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Infrastructure.SQL.Database;

namespace Tycoonia.Infrastructure.SQL.Repositories
{
    public class FactoryRepository : Repository<FactoryBase>, IFactoryRepository
    {
        public FactoryRepository(DbConnectionProvider provider) : base(provider) { }

        public async Task<FactoryBase?> GetByIdAsync(int id)
        {
            //Dictionary<int, FactoryBase> factories = [];
            using var connection = _connectionProvider.CreateConnection();

            SqlCommand selectFactoryCmd = new
            ("""
               SELECT * FROM Factories 
               WHERE Id = @Id
             """, connection);

            await connection.OpenAsync();
            using SqlDataReader reader = await selectFactoryCmd.ExecuteReaderAsync();

            int factoryIdIndex = reader.GetOrdinal("Id");
            int factoryLevelIndex = reader.GetOrdinal("Level");
            int factoryProductionRateIndex = reader.GetOrdinal("ProductionRate");

            if (!reader.Read()) return null;

            //int factoryId = reader.GetInt32(factoryIdIndex);

            //factories[factoryId] = new FactoryAluminum
            //{
            //    Id = factoryId,
            //    Level = reader.GetInt16(factoryLevelIndex),
            //    ProductionRate = reader.GetInt32(factoryProductionRateIndex)
            //};

            //return factories[factoryId];
            return new FactoryAluminum
            {
                Id = reader.GetInt32(factoryIdIndex),
                Level = reader.GetInt16(factoryLevelIndex),
                ProductionRate = reader.GetInt32(factoryProductionRateIndex)
            };
        }

        public async Task<IEnumerable<FactoryBase>> GetAllAsync()
        {
            List<FactoryBase> listFactories = [];
            using var connection = _connectionProvider.CreateConnection();
            SqlCommand selectFactoriesCmd = new
            ("""
                SELECT * 
                FROM Factories
             """, connection);

            await connection.OpenAsync();
            using var reader = await selectFactoriesCmd.ExecuteReaderAsync();

            int factoryIdIndex = reader.GetOrdinal("Id");
            int factoryLevelIndex = reader.GetOrdinal("Level");
            int factoryProductionRateIndex = reader.GetOrdinal("ProductionRate");

            while (reader.Read())
            {
                listFactories.Add(new FactoryAluminum
                {
                    Id = reader.GetInt32(factoryIdIndex),
                    Level = reader.GetInt16(factoryLevelIndex),
                    ProductionRate = reader.GetInt32(factoryProductionRateIndex)
                });
            }
            return listFactories;
        }

        public async Task AddAsync(FactoryBase factory)
        {
            using var connection = _connectionProvider.CreateConnection();
            using SqlTransaction transaction = connection.BeginTransaction();
            await connection.OpenAsync();
            try
            {
                SqlCommand insertFactoryCmd = new
                ("""
                    INSERT INTO Factories (Level, ProductionRate) 
                    VALUES (@Level, @ProductionRate)
                 """, connection, transaction);

                insertFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
                insertFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;

                await insertFactoryCmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(FactoryBase factory)
        {
            using var connection = _connectionProvider.CreateConnection();
            using SqlTransaction transaction = connection.BeginTransaction();
            await connection.OpenAsync();
            try
            {
                SqlCommand updateFactoryCmd = new
                 ("""
                    UPDATE Factories 
                    SET Level=@Level, ProductionRate=@ProductionRate 
                    WHERE Id=@Id
                 """, connection, transaction);

                updateFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
                updateFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;
                updateFactoryCmd.Parameters.Add("@Id", SqlDbType.Int).Value = factory.Id;

                await updateFactoryCmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connectionProvider.CreateConnection();
            using SqlTransaction transaction = connection.BeginTransaction();
            await connection.OpenAsync();
            try
            {
                SqlCommand deleteFactoryCmd = new
                ("""
                    DELETE FROM Factories 
                    WHERE Id=@Id
                 """, connection, transaction);
                deleteFactoryCmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                await deleteFactoryCmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpgradeFactoryAsync(int factoryId)
        {
            using var connection = _connectionProvider.CreateConnection();
            using SqlTransaction transaction = connection.BeginTransaction();
            await connection.OpenAsync();
            try
            {
                SqlCommand updateFactoryLvlCmd = new
                ("""
                    UPDATE Factories 
                    SET Level = Level + 1 
                    WHERE Id=@Id
                 """, connection, transaction);

                updateFactoryLvlCmd.Parameters.Add("@Id", SqlDbType.Int).Value = factoryId;

                await updateFactoryLvlCmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
