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

            SqlCommand cmd = new
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
     
                    FactoriesTypes.Type AS FactoryType,

                    FactoriesRecipeUpgradeList.Name AS RecipeName,
                    FactoriesRecipeUpgradeList.Amount AS RecipeAmount,

                    FactoriesResourceBuffer.Name AS BufferName,

                    FRBStorageResourcesBase.CurrentQuantity,
                    FRBStorageResourcesBase.MaxCapacity,
                    FRBStorageResourcesBase.UpgradeCost,
                    FRBStorageResourcesBase.Level AS BufferLevel,
                    FRBStorageResourcesBase.Price,

                    FactoriesProductionItemList.Name AS ProductionName,
                    FactoriesProductionItemList.Amount AS ProductionAmount
     
               FROM Factories
               JOIN FactoriesTypes ON FactoriesTypes.FactoryId = Factories.Id
               JOIN FactoriesRecipeUpgradeList ON FactoriesRecipeUpgradeList.FactoryId = Factories.Id
               LEFT JOIN FactoriesResourceBuffer ON FactoriesResourceBuffer.FactoryId = Factories.Id
               LEFT JOIN FRBStorageResourcesBase ON FRBStorageResourcesBase.ResourceBufferId = FactoriesResourceBuffer.Id
               JOIN FactoriesProductionItemList ON FactoriesProductionItemList.FactoryId = Factories.Id
               WHERE Factories.Id = @Id
             """, connection);

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            int factoryIdIndex = reader.GetOrdinal("Id");
            int factoryLevelIndex = reader.GetOrdinal("Level");
            int factoryProductionRateIndex = reader.GetOrdinal("ProductionRate");
            int factoryEnergyConsumptionIndex = reader.GetOrdinal("EnergyConsumption");
            int factoryProductionTimeIndex = reader.GetOrdinal("ProductionTime");
            int factoryProductionTimePerIterationIndex = reader.GetOrdinal("ProductionTimePerIteration");
            int factoryWorkFlagIndex = reader.GetOrdinal("WorkFlag");

            int factoryTypeIndex = reader.GetOrdinal("FactoryType");

            int recipeNameIndex = reader.GetOrdinal("RecipeName");
            int recipeAmountIndex = reader.GetOrdinal("RecipeAmount");

            int bufferNameIndex = reader.GetOrdinal("BufferName");

            int bufferCurrentIndex = reader.GetOrdinal("CurrentQuantity");
            int bufferMaxIndex = reader.GetOrdinal("MaxCapacity");
            int bufferUpgradeCostIndex = reader.GetOrdinal("UpgradeCost");
            int bufferLevelIndex = reader.GetOrdinal("BufferLevel");
            int bufferPriceIndex = reader.GetOrdinal("Price");

            int productionNameIndex = reader.GetOrdinal("ProductionName");
            int productionAmountIndex = reader.GetOrdinal("ProductionAmount");

            FactoryBase? factory = null;

            while (await reader.ReadAsync())
            {
                if (factory == null)
                {
                    string factoryType = reader.GetString(factoryTypeIndex);

                    factory = factoryType switch
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
                }

                string recipeName = reader.GetString(recipeNameIndex);
                long recipeAmount = reader.GetInt64(recipeAmountIndex);

                factory.RecipeUpgradeList[recipeName] = recipeAmount;

                if (!reader.IsDBNull(bufferNameIndex))
                {
                    string bufferName = reader.GetString(bufferNameIndex);

                    if (!factory.ResourceBuffer.ContainsKey(bufferName))
                    {
                        factory.ResourceBuffer[bufferName] = new StorageResourcesBase
                        {
                            CurrentQuantity = reader.IsDBNull(bufferCurrentIndex) ? 0 : reader.GetInt64(bufferCurrentIndex),
                            MaxCapacity = reader.IsDBNull(bufferMaxIndex) ? 0 : reader.GetInt64(bufferMaxIndex),
                            UpgradeCost = reader.IsDBNull(bufferUpgradeCostIndex) ? 0 : reader.GetInt64(bufferUpgradeCostIndex),
                            Level = reader.IsDBNull(bufferLevelIndex) ? (short)0 : reader.GetInt16(bufferLevelIndex),
                            Price = reader.IsDBNull(bufferPriceIndex) ? 0 : reader.GetInt32(bufferPriceIndex)
                        };
                    }
                }

                string productionName = reader.GetString(productionNameIndex);
                int productionAmount = reader.GetInt32(productionAmountIndex);

                factory.ProductionItemList[productionName] = productionAmount;
            }

            return factory;
        }

        public async Task<IEnumerable<FactoryBase>> GetAllAsync()
        {
            Dictionary<int, FactoryBase> factories = new();

            using var connection = _connectionProvider.CreateConnection();

            SqlCommand cmd = new
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
     
                    FactoriesTypes.Type AS FactoryType,
     
                    FactoriesRecipeUpgradeList.Name AS FactoryRecipeUpgradeListName,
                    FactoriesRecipeUpgradeList.Amount AS FactoryRecipeUpgradeListAmount,
     
                    FactoriesResourceBuffer.Name AS FactoryResourceBufferName,
     
                    FRBStorageResourcesBase.CurrentQuantity,
                    FRBStorageResourcesBase.MaxCapacity,
                    FRBStorageResourcesBase.UpgradeCost,
                    FRBStorageResourcesBase.Level AS BufferLevel,
                    FRBStorageResourcesBase.Price,
     
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
            using var reader = await cmd.ExecuteReaderAsync();

            int factoryIdIndex = reader.GetOrdinal("Id");
            int factoryNameIndex = reader.GetOrdinal("Name");
            int factoryLevelIndex = reader.GetOrdinal("Level");
            int factoryProductionRateIndex = reader.GetOrdinal("ProductionRate");
            int factoryEnergyConsumptionIndex = reader.GetOrdinal("EnergyConsumption");
            int factoryProductionTimeIndex = reader.GetOrdinal("ProductionTime");
            int factoryProductionTimePerIterationIndex = reader.GetOrdinal("ProductionTimePerIteration");
            int factoryWorkFlagIndex = reader.GetOrdinal("WorkFlag");

            int factoryTypeIndex = reader.GetOrdinal("FactoryType");

            int recipeNameIndex = reader.GetOrdinal("FactoryRecipeUpgradeListName");
            int recipeAmountIndex = reader.GetOrdinal("FactoryRecipeUpgradeListAmount");

            int bufferNameIndex = reader.GetOrdinal("FactoryResourceBufferName");

            int bufferCurrentIndex = reader.GetOrdinal("CurrentQuantity");
            int bufferMaxIndex = reader.GetOrdinal("MaxCapacity");
            int bufferUpgradeCostIndex = reader.GetOrdinal("UpgradeCost");
            int bufferLevelIndex = reader.GetOrdinal("BufferLevel");
            int bufferPriceIndex = reader.GetOrdinal("Price");

            int productionNameIndex = reader.GetOrdinal("FactoryProductionItemListName");
            int productionAmountIndex = reader.GetOrdinal("FactoryProductionItemListAmount");

            while (await reader.ReadAsync())
            {
                int factoryId = reader.GetInt32(factoryIdIndex);

                if (!factories.TryGetValue(factoryId, out FactoryBase factory))
                {
                    string factoryType = reader.GetString(factoryTypeIndex);

                    factory = factoryType switch
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

                    factory.Id = factoryId;
                    factory.Level = reader.GetInt16(factoryLevelIndex);
                    factory.ProductionRate = reader.GetInt32(factoryProductionRateIndex);
                    factory.EnergyConsumption = reader.GetDecimal(factoryEnergyConsumptionIndex);
                    factory.ProductionTime = reader.GetDecimal(factoryProductionTimeIndex);
                    factory.ProductionTimePerIteration = reader.GetDecimal(factoryProductionTimePerIterationIndex);
                    factory.WorkFlag = reader.GetBoolean(factoryWorkFlagIndex);

                    factories.Add(factoryId, factory);
                }

                string recipeName = reader.GetString(recipeNameIndex);
                long recipeAmount = reader.GetInt64(recipeAmountIndex);

                factory.RecipeUpgradeList[recipeName] = recipeAmount;

                if (!reader.IsDBNull(bufferNameIndex))
                {
                    string bufferName = reader.GetString(bufferNameIndex);

                    if (!factory.ResourceBuffer.ContainsKey(bufferName))
                    {
                        factory.ResourceBuffer[bufferName] = new StorageResourcesBase
                        {
                            CurrentQuantity = reader.IsDBNull(bufferCurrentIndex) ? 0 : reader.GetInt64(bufferCurrentIndex),
                            MaxCapacity = reader.IsDBNull(bufferMaxIndex) ? 0 : reader.GetInt64(bufferMaxIndex),
                            UpgradeCost = reader.IsDBNull(bufferUpgradeCostIndex) ? 0 : reader.GetInt64(bufferUpgradeCostIndex),
                            Level = reader.IsDBNull(bufferLevelIndex) ? (short)0 : reader.GetInt16(bufferLevelIndex),
                            Price = reader.IsDBNull(bufferPriceIndex) ? 0 : reader.GetInt32(bufferPriceIndex)
                        };
                    }
                }

                string productionName = reader.GetString(productionNameIndex);
                int productionAmount = reader.GetInt32(productionAmountIndex);

                factory.ProductionItemList[productionName] = productionAmount;
            }

            return factories.Values;
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
                    SET Level=@Level, ProductionRate=@ProductionRate, EnergyConsumption=@EnergyConsumption, WorkFlag=@WorkFlag, ProductionTime=@ProductionTime, ProductionTimePerIteration=@ProductionTimePerIteration 
                    WHERE Id=@Id
                 """, connection, transaction);

                updateFactoryCmd.Parameters.Add("@Level", SqlDbType.SmallInt).Value = factory.Level;
                updateFactoryCmd.Parameters.Add("@ProductionRate", SqlDbType.Int).Value = factory.ProductionRate;
                updateFactoryCmd.Parameters.Add("@EnergyConsumption", SqlDbType.Decimal).Value = factory.EnergyConsumption;
                updateFactoryCmd.Parameters.Add("@Id", SqlDbType.Int).Value = factory.Id;
                updateFactoryCmd.Parameters.Add("@WorkFlag", SqlDbType.Bit).Value = factory.WorkFlag;
                updateFactoryCmd.Parameters.Add("@ProductionTime", SqlDbType.Decimal).Value = factory.ProductionTime;
                updateFactoryCmd.Parameters.Add("@ProductionTimePerIteration", SqlDbType.Decimal).Value = factory.ProductionTimePerIteration;
                await updateFactoryCmd.ExecuteNonQueryAsync();

                SqlCommand updateFactoryRecipeUpgradeListCmd = new
                ("""
                    UPDATE FactoriesRecipeUpgradeList 
                    SET Amount=@Amount
                    WHERE FactoryId=@Id AND Name=@Name
                 """, connection, transaction);

                updateFactoryRecipeUpgradeListCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
                updateFactoryRecipeUpgradeListCmd.Parameters.Add("@Amount", SqlDbType.Int);
                updateFactoryRecipeUpgradeListCmd.Parameters.Add("@Id", SqlDbType.Int).Value = factory.Id;

                foreach (var item in factory.RecipeUpgradeList)
                {
                    updateFactoryRecipeUpgradeListCmd.Parameters["@Name"].Value = item.Key;
                    updateFactoryRecipeUpgradeListCmd.Parameters["@Amount"].Value = item.Value;
                    await updateFactoryRecipeUpgradeListCmd.ExecuteNonQueryAsync();
                }

                SqlCommand updateFactoryProductionItemListCmd = new
                ("""
                    UPDATE FactoriesProductionItemList 
                    SET Amount=@Amount
                    WHERE FactoryId=@Id AND Name=@Name
                 """, connection, transaction);

                updateFactoryProductionItemListCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
                updateFactoryProductionItemListCmd.Parameters.Add("@Amount", SqlDbType.Int);
                updateFactoryProductionItemListCmd.Parameters.Add("@Id", SqlDbType.Int).Value = factory.Id;

                foreach (var item in factory.ProductionItemList)
                {
                    updateFactoryProductionItemListCmd.Parameters["@Name"].Value = item.Key;
                    updateFactoryProductionItemListCmd.Parameters["@Amount"].Value = item.Value;
                    await updateFactoryProductionItemListCmd.ExecuteNonQueryAsync();
                }
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
