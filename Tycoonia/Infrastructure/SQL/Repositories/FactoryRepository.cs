using Microsoft.Data.SqlClient;
using System.Data;
using Tycoonia.Application.Interfaces;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Infrastructure.SQL.Database;

namespace Tycoonia.Infrastructure.SQL.Repositories
{
    public class FactoryRepository : Repository<FactoryBase>, IRepository<FactoryBase>
    {
        public FactoryRepository(DbConnectionProvider provider) : base(provider) { }

        public async Task<FactoryBase?> GetByIdAsync(int id)
        {
            using var connection = _connectionProvider.CreateConnection();

            SqlCommand selectFactoryCmd = new
            ("""
               SELECT * FROM Factories 
               WHERE Id = @Id
             """, connection);

            selectFactoryCmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            using SqlDataReader reader = await selectFactoryCmd.ExecuteReaderAsync();

            int factoryIdIndex = reader.GetOrdinal("Id");
            int factoryLevelIndex = reader.GetOrdinal("Level");
            int factoryProductionRateIndex = reader.GetOrdinal("ProductionRate");
            int factoryEnergyConsumptionIndex = reader.GetOrdinal("EnergyConsumption");
            int factoryWorkFlagIndex = reader.GetOrdinal("WorkFlag");

            if (!reader.Read()) return null;

            string factoryType = reader.GetString(reader.GetOrdinal("Type"));
            FactoryBase factory = factoryType switch
            {
                "Aluminum" => new FactoryAluminum(),
                "Batteries" => new FactoryBatteries(),
                "Bricks" => new FactoryBricks(),
                "Concrete" => new FactoryConcrete(),
                "Copper Wire" => new FactoryCopperWire(),
                "Diamonds" => new FactoryDiamonds(),
                "Electronic Components" => new FactoryElectronicComponents(),
                "Energy Storage" => new FactoryEnergyStorage(),
                "Uranium X" => new FactoryEnrichmentUranium(),
                "Fuel" => new FactoryFuel(),
                "Glass" => new FactoryGlass(),
                "Gold Bars" => new FactoryGoldBars(),
                "Plastic" => new FactoryPlastic(),
                "Purified Lithium" => new FactoryPurifiedLithium(),
                "Silicon" => new FactorySilicon(),
                "Silver Bars" => new FactorySilverBars(),
                "Solid Fuel" => new FactorySolidFuel(),
                "Steel" => new FactorySteel(),
                "Thorium Rod" => new FactoryThoriumRod(),
                "Titanium" => new FactoryTitanium(),
                "Uranium Rod" => new FactoryUraniumRod(),
                _ => throw new InvalidOperationException($"Unknown factory type: {factoryType}")
            };
            factory.Id = reader.GetInt32(factoryIdIndex);
            factory.Level = reader.GetInt16(factoryLevelIndex);
            factory.ProductionRate = reader.GetInt32(factoryProductionRateIndex);
            factory.EnergyConsumption = reader.GetDecimal(factoryEnergyConsumptionIndex);
            factory.WorkFlag = reader.GetBoolean(factoryWorkFlagIndex);

            return factory;
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
            int factoryEnergyConsumptionIndex = reader.GetOrdinal("EnergyConsumption");
            int factoryWorkFlagIndex = reader.GetOrdinal("WorkFlag");

            while (reader.Read())
            {
                string factoryType = reader.GetString(reader.GetOrdinal("Type"));
                FactoryBase factory = factoryType switch
                {
                    "Aluminum" => new FactoryAluminum(),
                    "Batteries" => new FactoryBatteries(),
                    "Bricks" => new FactoryBricks(),
                    "Concrete" => new FactoryConcrete(),
                    "Copper Wire" => new FactoryCopperWire(),
                    "Diamonds" => new FactoryDiamonds(),
                    "Electronic Components" => new FactoryElectronicComponents(),
                    "Energy Storage" => new FactoryEnergyStorage(),
                    "Uranium X" => new FactoryEnrichmentUranium(),
                    "Fuel" => new FactoryFuel(),
                    "Glass" => new FactoryGlass(),
                    "Gold Bars" => new FactoryGoldBars(),
                    "Plastic" => new FactoryPlastic(),
                    "Purified Lithium" => new FactoryPurifiedLithium(),
                    "Silicon" => new FactorySilicon(),
                    "Silver Bars" => new FactorySilverBars(),
                    "Solid Fuel" => new FactorySolidFuel(),
                    "Steel" => new FactorySteel(),
                    "Thorium Rod" => new FactoryThoriumRod(),
                    "Titanium" => new FactoryTitanium(),
                    "Uranium Rod" => new FactoryUraniumRod(),
                    _ => throw new InvalidOperationException($"Unknown factory type: {factoryType}")
                };
                factory.Id = reader.GetInt32(factoryIdIndex);
                factory.Level = reader.GetInt16(factoryLevelIndex);
                factory.ProductionRate = reader.GetInt32(factoryProductionRateIndex);
                factory.EnergyConsumption = reader.GetDecimal(factoryEnergyConsumptionIndex);
                factory.WorkFlag = reader.GetBoolean(factoryWorkFlagIndex);
                listFactories.Add(factory);
            }
            return listFactories;
        }

        public async Task AddAsync(FactoryBase factory)
        {
            using var connection = _connectionProvider.CreateConnection();
            await connection.OpenAsync();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand insertFactoryCmd = new
                ("""
                    INSERT INTO Factories (Type, Name, Level, ProductionRate, EnergyConsumption, WorkFlag) 
                    VALUES (@Type, @Name, @Level, @ProductionRate, @EnergyConsumption, @WorkFlag)
                 """, connection, transaction);

                insertFactoryCmd.Parameters.Add("@Type", SqlDbType.NVarChar, 100).Value = factory.Type; 
                insertFactoryCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = factory.Name;
                insertFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
                insertFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;
                insertFactoryCmd.Parameters.Add("@EnergyConsumption", SqlDbType.Decimal).Value = factory.EnergyConsumption;
                insertFactoryCmd.Parameters.Add("@WorkFlag", SqlDbType.Bit).Value = factory.WorkFlag;

                await insertFactoryCmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(FactoryBase factory)
        {
            using var connection = _connectionProvider.CreateConnection();
            await connection.OpenAsync();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                SqlCommand updateFactoryCmd = new
                 ("""
                    UPDATE Factories 
                    SET Type=@Type, Name=@Name, Level=@Level, ProductionRate=@ProductionRate, EnergyConsumption=@EnergyConsumption, WorkFlag=@WorkFlag 
                    WHERE Id=@Id
                 """, connection, transaction);

                updateFactoryCmd.Parameters.Add("@Type", SqlDbType.NVarChar, 100).Value = factory.Type;
                updateFactoryCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = factory.Name;
                updateFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
                updateFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;
                updateFactoryCmd.Parameters.Add("@EnergyConsumption", SqlDbType.Decimal).Value = factory.EnergyConsumption;
                updateFactoryCmd.Parameters.Add("@WorkFlag", SqlDbType.Bit).Value = factory.WorkFlag;
                updateFactoryCmd.Parameters.Add("@Id", SqlDbType.Int).Value = factory.Id;

                await updateFactoryCmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connectionProvider.CreateConnection();
            await connection.OpenAsync();
            using SqlTransaction transaction = connection.BeginTransaction();
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
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpgradeFactoryAsync(int factoryId)
        {
            using var connection = _connectionProvider.CreateConnection();
            await connection.OpenAsync();
            using SqlTransaction transaction = connection.BeginTransaction();
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
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> AnyAsync()
        {
            using var connection = _connectionProvider.CreateConnection();

            SqlCommand checkAnyCmd = new
            ("""
                SELECT TOP 1 Id FROM Factories
             """, connection);
            
            await connection.OpenAsync();

            using var reader = await checkAnyCmd.ExecuteReaderAsync();
            return reader.Read();
        }

        //public async Task UpdateByIdAsync(int id)
        //{
        //    using var connection = _connectionProvider.CreateConnection();
        //    await connection.OpenAsync();
        //    using SqlTransaction transaction = connection.BeginTransaction();
        //    try
        //    {
        //        SqlCommand updateByIdFactoryCmd = new
        //         ("""
        //            UPDATE Factories 
        //            SET Type=@Type, Name=@Name, Level=@Level, ProductionRate=@ProductionRate, EnergyConsumption=@EnergyConsumption, WorkFlag=@WorkFlag 
        //            WHERE Id=@Id
        //         """, connection, transaction);

        //        var factory = await GetByIdAsync(id);

        //        updateByIdFactoryCmd.Parameters.Add("@Type", SqlDbType.NVarChar, 100).Value = factory.Type;
        //        updateByIdFactoryCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = factory.Name;
        //        updateByIdFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
        //        updateByIdFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;
        //        updateByIdFactoryCmd.Parameters.Add("@EnergyConsumption", SqlDbType.Decimal).Value = factory.EnergyConsumption;
        //        updateByIdFactoryCmd.Parameters.Add("@WorkFlag", SqlDbType.Bit).Value = factory.WorkFlag;
        //        updateByIdFactoryCmd.Parameters.Add("@Id", SqlDbType.Int).Value = factory.Id;

        //        await updateByIdFactoryCmd.ExecuteNonQueryAsync();
        //        await transaction.CommitAsync();
        //    }
        //    catch
        //    {
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //}
    }
}
