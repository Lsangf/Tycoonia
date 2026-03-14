using Microsoft.Data.SqlClient;
using System.Data;
using Tycoonia.Application.Interfaces;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;
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
               SELECT 
                    Factories.Id,
                    Factories.Name, 
                    Factories.Level,
                    Factories.ProductionRate,
                    Factories.EnergyConsumption,
                    Factories.ProductionTime,
                    Factories.ProductionTimePerIteration,
                    Factories.WorkFlag,
             
                    FactoriesTypes.Id AS FactoryTypeId,
                    FactoriesTypes.Type,

                    FactoriesRecipeUpgradeList.Id AS FactoryRecipeUpgradeListId,
                    FactoriesRecipeUpgradeList.Name AS FactoryRecipeUpgradeListName,
                    FactoriesRecipeUpgradeList.Amount AS FactoryRecipeUpgradeListAmount,

                    FactoriesResourceBuffer.Id AS FactoryResourceBufferId,
                    FactoriesResourceBuffer.Name AS FactoryResourceBufferName,

                    FRBStorageResourcesBase.Id AS FRBStorageResourcesBaseId,
                    FRBStorageResourcesBase.CurrentQuantity AS FRBStorageResourcesBaseCurrentQuantity,
                    FRBStorageResourcesBase.MaxCapacity AS FRBStorageResourcesBaseMaxCapacity,
                    FRBStorageResourcesBase.UpgradeCost AS FRBStorageResourcesBaseUpgradeCost,
                    FRBStorageResourcesBase.Level AS FRBStorageResourcesBaseLevel,
                    FRBStorageResourcesBase.Price AS FRBStorageResourcesBasePrice,

                    FactoriesProductionItemList.Id AS FactoryProductionItemListId,
                    FactoriesProductionItemList.Name AS FactoryProductionItemListName,
                    FactoriesProductionItemList.Amount AS FactoryProductionItemListAmount
             
               FROM Factories
               JOIN FactoriesTypes ON FactoriesTypes.FactoryId = Factories.Id
               JOIN FactoriesRecipeUpgradeList ON FactoriesRecipeUpgradeList.FactoryId = Factories.Id
               LEFT JOIN FactoriesResourceBuffer ON FactoriesResourceBuffer.FactoryId = Factories.Id
               LEFT JOIN FRBStorageResourcesBase ON FRBStorageResourcesBase.ResourceBufferId = FactoriesResourceBuffer.Id
               JOIN FactoriesProductionItemList ON FactoriesProductionItemList.FactoryId = Factories.Id
               WHERE Id = @Id
             """, connection);

            selectFactoryCmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            using SqlDataReader reader = await selectFactoryCmd.ExecuteReaderAsync();

            int factoryIdIndex = reader.GetOrdinal("Id");
            int factoryNameIndex = reader.GetOrdinal("Name");
            int factoryLevelIndex = reader.GetOrdinal("Level");
            int factoryProductionRateIndex = reader.GetOrdinal("ProductionRate");
            int factoryEnergyConsumptionIndex = reader.GetOrdinal("EnergyConsumption");
            int factoryProductionTimeIndex = reader.GetOrdinal("ProductionTime");
            int factoryProductionTimePerIterationIndex = reader.GetOrdinal("ProductionTimePerIteration");
            int factoryWorkFlagIndex = reader.GetOrdinal("WorkFlag");

            int factoryTypeTypeIndex = reader.GetOrdinal("Type");

            int factoryRecipeUpgradeListIdIndex = reader.GetOrdinal("FactoryRecipeUpgradeListId");
            int factoryRecipeUpgradeListNameIndex = reader.GetOrdinal("FactoryRecipeUpgradeListName");
            int factoryRecipeUpgradeListAmountIndex = reader.GetOrdinal("FactoryRecipeUpgradeListAmount");

            int factoryResourceBufferIdIndex = reader.GetOrdinal("FactoryResourceBufferId");
            int factoryResourceBufferNameIndex = reader.GetOrdinal("FactoryResourceBufferName");

            int frbStorageResourcesBaseIdIndex = reader.GetOrdinal("FRBStorageResourcesBaseId");
            int frbStorageResourcesBaseCurrentQuantityIndex = reader.GetOrdinal("FRBStorageResourcesBaseCurrentQuantity");
            int frbStorageResourcesBaseMaxCapacityIndex = reader.GetOrdinal("FRBStorageResourcesBaseMaxCapacity");
            int frbStorageResourcesBaseUpgradeCostIndex = reader.GetOrdinal("FRBStorageResourcesBaseUpgradeCost");
            int frbStorageResourcesBaseLevelIndex = reader.GetOrdinal("FRBStorageResourcesBaseLevel");
            int frbStorageResourcesBasePriceIndex = reader.GetOrdinal("FRBStorageResourcesBasePrice");

            int factoryProductionItemListIdIndex = reader.GetOrdinal("FactoryProductionItemListId");
            int factoryProductionItemListNameIndex = reader.GetOrdinal("FactoryProductionItemListName");
            int factoryProductionItemListAmountIndex = reader.GetOrdinal("FactoryProductionItemListAmount");

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
                "Uranium-235" => new FactoryEnrichmentUranium(),
                "Uranium-238" => new FactoryEnrichmentUranium(),
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
            factory.ProductionTime = reader.GetDecimal(factoryProductionTimeIndex);
            factory.ProductionTimePerIteration = reader.GetDecimal(factoryProductionTimePerIterationIndex);
            factory.WorkFlag = reader.GetBoolean(factoryWorkFlagIndex);

            while (await reader.ReadAsync())
            {
                factory.RecipeUpgradeList[reader.GetString(factoryRecipeUpgradeListNameIndex)] = reader.GetInt64(factoryRecipeUpgradeListAmountIndex);
                if (!reader.IsDBNull(factoryResourceBufferNameIndex))
                {
                    string bufferName = reader.GetString(factoryResourceBufferNameIndex);

                    factory.ResourceBuffer.Add(bufferName,
                        new StorageResourcesBase
                        {
                            CurrentQuantity = reader.IsDBNull(frbStorageResourcesBaseCurrentQuantityIndex)
                                ? 0
                                : reader.GetInt64(frbStorageResourcesBaseCurrentQuantityIndex),

                            MaxCapacity = reader.IsDBNull(frbStorageResourcesBaseMaxCapacityIndex)
                                ? 0
                                : reader.GetInt64(frbStorageResourcesBaseMaxCapacityIndex),

                            UpgradeCost = reader.IsDBNull(frbStorageResourcesBaseUpgradeCostIndex)
                                ? 0
                                : reader.GetInt64(frbStorageResourcesBaseUpgradeCostIndex),

                            Level = reader.IsDBNull(frbStorageResourcesBaseLevelIndex)
                                ? (short)0
                                : reader.GetInt16(frbStorageResourcesBaseLevelIndex),

                            Price = reader.IsDBNull(frbStorageResourcesBasePriceIndex)
                                ? 0
                                : reader.GetInt32(frbStorageResourcesBasePriceIndex)
                        });
                }
                factory.ProductionItemList[reader.GetString(factoryProductionItemListNameIndex)] = reader.GetInt32(factoryProductionItemListAmountIndex);
            }
            return factory;
        }

        public async Task<IEnumerable<FactoryBase>> GetAllAsync()
        {
            List<FactoryBase> listFactories = [];
            using var connection = _connectionProvider.CreateConnection();
            SqlCommand selectFactoriesCmd = new
            ("""
                SELECT 
                    Factories.Id,
                    Factories.Name, 
                    Factories.Level,
                    Factories.ProductionRate,
                    Factories.EnergyConsumption,
                    Factories.ProductionTime,
                    Factories.ProductionTimePerIteration,
                    Factories.WorkFlag,
             
                    FactoriesTypes.Id AS FactoryTypeId,
                    FactoriesTypes.Type,
             
                    FactoriesRecipeUpgradeList.Id AS FactoryRecipeUpgradeListId,
                    FactoriesRecipeUpgradeList.Name AS FactoryRecipeUpgradeListName,
                    FactoriesRecipeUpgradeList.Amount AS FactoryRecipeUpgradeListAmount,
             
                    FactoriesResourceBuffer.Id AS FactoryResourceBufferId,
                    FactoriesResourceBuffer.Name AS FactoryResourceBufferName,
             
                    FRBStorageResourcesBase.Id AS FRBStorageResourcesBaseId,
                    FRBStorageResourcesBase.CurrentQuantity AS FRBStorageResourcesBaseCurrentQuantity,
                    FRBStorageResourcesBase.MaxCapacity AS FRBStorageResourcesBaseMaxCapacity,
                    FRBStorageResourcesBase.UpgradeCost AS FRBStorageResourcesBaseUpgradeCost,
                    FRBStorageResourcesBase.Level AS FRBStorageResourcesBaseLevel,
                    FRBStorageResourcesBase.Price AS FRBStorageResourcesBasePrice,
             
                    FactoriesProductionItemList.Id AS FactoryProductionItemListId,
                    FactoriesProductionItemList.Name AS FactoryProductionItemListName,
                    FactoriesProductionItemList.Amount AS FactoryProductionItemListAmount
             
               FROM Factories
               JOIN FactoriesTypes ON FactoriesTypes.FactoryId = Factories.Id
               JOIN FactoriesRecipeUpgradeList ON FactoriesRecipeUpgradeList.FactoryId = Factories.Id
               LEFT JOIN FactoriesResourceBuffer ON FactoriesResourceBuffer.FactoryId = Factories.Id
               LEFT JOIN FRBStorageResourcesBase ON FRBStorageResourcesBase.ResourceBufferId = FactoriesResourceBuffer.Id
               JOIN FactoriesProductionItemList ON FactoriesProductionItemList.FactoryId = Factories.Id
               ORDER BY Factories.Id
             """, connection);

            await connection.OpenAsync();
            using var reader = await selectFactoriesCmd.ExecuteReaderAsync();

            int factoryIdIndex = reader.GetOrdinal("Id");
            int factoryNameIndex = reader.GetOrdinal("Name");
            int factoryLevelIndex = reader.GetOrdinal("Level");
            int factoryProductionRateIndex = reader.GetOrdinal("ProductionRate");
            int factoryEnergyConsumptionIndex = reader.GetOrdinal("EnergyConsumption");
            int factoryProductionTimeIndex = reader.GetOrdinal("ProductionTime");
            int factoryProductionTimePerIterationIndex = reader.GetOrdinal("ProductionTimePerIteration");
            int factoryWorkFlagIndex = reader.GetOrdinal("WorkFlag");

            int factoryTypeTypeIndex = reader.GetOrdinal("Type");

            int factoryRecipeUpgradeListIdIndex = reader.GetOrdinal("FactoryRecipeUpgradeListId");
            int factoryRecipeUpgradeListNameIndex = reader.GetOrdinal("FactoryRecipeUpgradeListName");
            int factoryRecipeUpgradeListAmountIndex = reader.GetOrdinal("FactoryRecipeUpgradeListAmount");

            int factoryResourceBufferIdIndex = reader.GetOrdinal("FactoryResourceBufferId");
            int factoryResourceBufferNameIndex = reader.GetOrdinal("FactoryResourceBufferName");

            int frbStorageResourcesBaseIdIndex = reader.GetOrdinal("FRBStorageResourcesBaseId");
            int frbStorageResourcesBaseCurrentQuantityIndex = reader.GetOrdinal("FRBStorageResourcesBaseCurrentQuantity");
            int frbStorageResourcesBaseMaxCapacityIndex = reader.GetOrdinal("FRBStorageResourcesBaseMaxCapacity");
            int frbStorageResourcesBaseUpgradeCostIndex = reader.GetOrdinal("FRBStorageResourcesBaseUpgradeCost");
            int frbStorageResourcesBaseLevelIndex = reader.GetOrdinal("FRBStorageResourcesBaseLevel");
            int frbStorageResourcesBasePriceIndex = reader.GetOrdinal("FRBStorageResourcesBasePrice");

            int factoryProductionItemListIdIndex = reader.GetOrdinal("FactoryProductionItemListId");
            int factoryProductionItemListNameIndex = reader.GetOrdinal("FactoryProductionItemListName");
            int factoryProductionItemListAmountIndex = reader.GetOrdinal("FactoryProductionItemListAmount");

            while (await reader.ReadAsync())
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
                    "Uranium-235" => new FactoryEnrichmentUranium(),
                    "Uranium-238" => new FactoryEnrichmentUranium(),
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
                factory.ProductionTime = reader.GetDecimal(factoryProductionTimeIndex);
                factory.ProductionTimePerIteration = reader.GetDecimal(factoryProductionTimePerIterationIndex);
                factory.WorkFlag = reader.GetBoolean(factoryWorkFlagIndex);

                while (await reader.ReadAsync())
                {
                    factory.RecipeUpgradeList[reader.GetString(factoryRecipeUpgradeListNameIndex)] = reader.GetInt64(factoryRecipeUpgradeListAmountIndex);
                    if (!reader.IsDBNull(factoryResourceBufferNameIndex))
                    {
                        string bufferName = reader.GetString(factoryResourceBufferNameIndex);

                        factory.ResourceBuffer.Add(bufferName,
                            new StorageResourcesBase
                            {
                                CurrentQuantity = reader.IsDBNull(frbStorageResourcesBaseCurrentQuantityIndex)
                                    ? 0
                                    : reader.GetInt64(frbStorageResourcesBaseCurrentQuantityIndex),

                                MaxCapacity = reader.IsDBNull(frbStorageResourcesBaseMaxCapacityIndex)
                                    ? 0
                                    : reader.GetInt64(frbStorageResourcesBaseMaxCapacityIndex),

                                UpgradeCost = reader.IsDBNull(frbStorageResourcesBaseUpgradeCostIndex)
                                    ? 0
                                    : reader.GetInt64(frbStorageResourcesBaseUpgradeCostIndex),

                                Level = reader.IsDBNull(frbStorageResourcesBaseLevelIndex)
                                    ? (short)0
                                    : reader.GetInt16(frbStorageResourcesBaseLevelIndex),

                                Price = reader.IsDBNull(frbStorageResourcesBasePriceIndex)
                                    ? 0
                                    : reader.GetInt32(frbStorageResourcesBasePriceIndex)
                            });
                    }
                    factory.ProductionItemList[reader.GetString(factoryProductionItemListNameIndex)] = reader.GetInt32(factoryProductionItemListAmountIndex);
                }
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
                    INSERT INTO Factories (Name, Level, ProductionRate, EnergyConsumption, ProductionTime, ProductionTimePerIteration, WorkFlag) 
                    OUTPUT INSERTED.Id
                    VALUES (@Name, @Level, @ProductionRate, @EnergyConsumption, @ProductionTime, @ProductionTimePerIteration, @WorkFlag)
                 """, connection, transaction);

                insertFactoryCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = factory.Name;
                insertFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
                insertFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;
                insertFactoryCmd.Parameters.Add("@EnergyConsumption", SqlDbType.Decimal).Value = factory.EnergyConsumption;
                insertFactoryCmd.Parameters.Add("@ProductionTime", SqlDbType.Decimal).Value = factory.ProductionTime;
                insertFactoryCmd.Parameters.Add("@ProductionTimePerIteration", SqlDbType.Decimal).Value = factory.ProductionTimePerIteration;
                insertFactoryCmd.Parameters.Add("@WorkFlag", SqlDbType.Bit).Value = factory.WorkFlag;

                int insertedFactoryId = (int)await insertFactoryCmd.ExecuteScalarAsync();

                SqlCommand insertFactoriesTypesCmd = new
                ("""
                    INSERT INTO FactoriesTypes (FactoryId, Type) 
                    VALUES (@FactoryId, @Type)
                 """, connection, transaction);

                insertFactoriesTypesCmd.Parameters.Add("@FactoryId", SqlDbType.Int);
                insertFactoriesTypesCmd.Parameters.Add("@Type", SqlDbType.NVarChar, 100);

                foreach (var type in factory.Type)
                {
                    insertFactoriesTypesCmd.Parameters["@FactoryId"].Value = insertedFactoryId;
                    insertFactoriesTypesCmd.Parameters["@Type"].Value = type;
                    await insertFactoriesTypesCmd.ExecuteNonQueryAsync();
                }

                SqlCommand insertFactoriesRecipeUpgradeListCmd = new
                ("""
                    INSERT INTO FactoriesRecipeUpgradeList (FactoryId, Name, Amount) 
                    VALUES (@FactoryId, @Name, @Amount)
                 """, connection, transaction);

                insertFactoriesRecipeUpgradeListCmd.Parameters.Add("@FactoryId", SqlDbType.Int).Value = insertedFactoryId;
                insertFactoriesRecipeUpgradeListCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150);
                insertFactoriesRecipeUpgradeListCmd.Parameters.Add("@Amount", SqlDbType.Int);

                foreach (var item in factory.RecipeUpgradeList)
                {
                    insertFactoriesRecipeUpgradeListCmd.Parameters["@Name"].Value = item.Key;
                    insertFactoriesRecipeUpgradeListCmd.Parameters["@Amount"].Value = item.Value;
                    await insertFactoriesRecipeUpgradeListCmd.ExecuteNonQueryAsync();
                }

                SqlCommand insertFactoriesResourceBufferCmd = new
                ("""
                    INSERT INTO FactoriesResourceBuffer (FactoryId, Name)
                    OUTPUT INSERTED.Id
                    VALUES (@FactoryId, @Name)
                 """, connection, transaction);

                insertFactoriesResourceBufferCmd.Parameters.Add("@FactoryId", SqlDbType.Int).Value = insertedFactoryId;
                insertFactoriesResourceBufferCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150);

                SqlCommand insertFRBStorageResourcesBaseCmd = new
                ("""
                    INSERT INTO FRBStorageResourcesBase (ResourceBufferId, CurrentQuantity, MaxCapacity, UpgradeCost, Level, Price)
                    VALUES (@ResourceBufferId, @CurrentQuantity, @MaxCapacity, @UpgradeCost, @Level, @Price)
                 """, connection, transaction);

                foreach (var item in factory.ResourceBuffer)
                {
                    insertFactoriesResourceBufferCmd.Parameters["@Name"].Value = item.Key;
                    int bufferId = (int)await insertFactoriesResourceBufferCmd.ExecuteScalarAsync();

                    insertFRBStorageResourcesBaseCmd.Parameters["@ResourceBufferId"].Value = bufferId;
                    insertFRBStorageResourcesBaseCmd.Parameters["@CurrentQuantity"].Value = item.Value.CurrentQuantity;
                    insertFRBStorageResourcesBaseCmd.Parameters["@MaxCapacity"].Value = item.Value.MaxCapacity;
                    insertFRBStorageResourcesBaseCmd.Parameters["@UpgradeCost"].Value = item.Value.UpgradeCost;
                    insertFRBStorageResourcesBaseCmd.Parameters["@Level"].Value = item.Value.Level;
                    insertFRBStorageResourcesBaseCmd.Parameters["@Price"].Value = item.Value.Price;

                    await insertFRBStorageResourcesBaseCmd.ExecuteNonQueryAsync();
                }

                SqlCommand insertFactoriesProductionItemListCmd = new
                ("""
                    INSERT INTO FactoriesProductionItemList (FactoryId, Name, Amount)
                    VALUES (@FactoryId, @Name, @Amount)
                 """, connection, transaction);

                insertFactoriesProductionItemListCmd.Parameters.Add("@FactoryId", SqlDbType.Int).Value = insertedFactoryId;
                insertFactoriesProductionItemListCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150);
                insertFactoriesProductionItemListCmd.Parameters.Add("@Amount", SqlDbType.Int);

                foreach (var item in factory.ProductionItemList)
                {
                    insertFactoriesProductionItemListCmd.Parameters["@Name"].Value = item.Key;
                    insertFactoriesProductionItemListCmd.Parameters["@Amount"].Value = item.Value;
                    await insertFactoriesProductionItemListCmd.ExecuteNonQueryAsync();
                }

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
                    SET Name=@Name, Level=@Level, EnergyConsumption=@EnergyConsumption, WorkFlag=@WorkFlag 
                    WHERE Id=@Id
                 """, connection, transaction);

                //updateFactoryCmd.Parameters.Add("@Type", SqlDbType.NVarChar, 100).Value = factory.Type;
                updateFactoryCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = factory.Name;
                updateFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
                //updateFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;
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
    }
}
