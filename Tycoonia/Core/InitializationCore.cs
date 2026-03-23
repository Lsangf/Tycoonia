using Tycoonia.Application.Interfaces;
using Tycoonia.Application.Services;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.NPP;
using Tycoonia.Domain.Buildings.EnergyPlant.TPP;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.EnergyResources;
using Tycoonia.Domain.Resources.ProcessedResources;
using Tycoonia.Domain.Resources.ProducedResources;
using Tycoonia.Domain.Resources.RawResources;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Infrastructure.SQL;
using Tycoonia.Infrastructure.SQL.Database;
using Tycoonia.Infrastructure.SQL.Repositories;

namespace Tycoonia.Core
{
    public class InitializationCore
    {
        private static Random random = new();
        public static async Task Main()
        {
            PlayerReal player = CreatePlayer();

            CoalTPP coalTPP = new();
            FuelTPP fuelTPP = new();
            SolidFuelTPP solidFuelTPP = new();
            UraniumNPP uraniumNPP = new();
            ThoriumNPP thoriumNPP = new();

            // storage
            StorageResources storageResources = new();
            Domain.Resources.Storage.EnergyStorage energyStorage = new();

            // gameloop

            List<MineBase> mines = null;

            List<FactoryBase> factories = null;

            List <EnergyPlantBase> energyPlants =
            [
                coalTPP, fuelTPP, solidFuelTPP, uraniumNPP, thoriumNPP
            ];

            string connectionString = await SQLInitializer.InitializeAsync();
            DbConnectionProvider connectionProvider = new(connectionString);

            IRepository<FactoryBase> factoryRepository = new FactoryRepository(connectionProvider);

            DataInitializer dataInitializer = new(factoryRepository);
            await dataInitializer.Initialize();

            FactoryService factoryService = new(factoryRepository);

            List<FactoryBase> factoriesDB = (await factoryService.GetAllFactoriesAsync()).ToList();
            foreach (var factory in factoriesDB)
            {
                switch (factory.Name)
                {
                    case "Aluminum Factory":
                        FactoryAluminum factoryAluminumDb = new();
                        break;
                    case "Batteries Factory":
                        FactoryBatteries factoryBatteriesDb = new();
                        break;
                    case "Bricks Factory":
                        FactoryBricks factoryBricksDb = new();
                        break;
                    case "Concrete Factory":
                        FactoryConcrete factoryConcreteDb = new();
                        break;
                    case "Copper Wire Factory":
                        FactoryCopperWire factoryCopperWireDb = new();
                        break;
                    case "Diamond Refinery":
                        FactoryDiamonds factoryDiamondsDb = new();
                        break;
                    case "Electronic Components Factory":
                        FactoryElectronicComponents factoryElectronicComponentsDb = new();
                        break;
                    case "Storage Factory":
                        FactoryEnergyStorage factoryEnergyStorageDb = new();
                        break;
                    case "Enrichment Factory":
                        FactoryEnrichmentUranium factoryEnrichmentUraniumDb = new();
                        break;
                    case "Fuel Factory":
                        FactoryFuel factoryFuelDb = new();
                        break;
                    case "Glass Factory":
                        FactoryGlass factoryGlassDb = new();
                        break;
                    case "Gold Bars Factory":
                        FactoryGoldBars factoryGoldBarsDb = new();
                        break;
                    case "Plastic Factory":
                        FactoryPlastic factoryPlasticDb = new();
                        break;
                    case "Purified Lithium Factory":
                        FactoryPurifiedLithium factoryPurifiedLithiumDb = new();
                        break;
                    case "Silicon Factory":
                        FactorySilicon factorySiliconDb = new();
                        break;
                    case "Silver Bars Factory":
                        FactorySilverBars factorySilverBarsDb = new();
                        break;
                    case "Solid Fuel Factory":
                        FactorySolidFuel factorySolidFuelDb = new();
                        break;
                    case "Steel Factory":
                        FactorySteel factorySteelDb = new();
                        break;
                    case "Thorium Rod Factory":
                        FactoryThoriumRod factoryThoriumRodDb = new();
                        break;
                    case "Titanium Factory":
                        FactoryTitanium factoryTitaniumDb = new();
                        break;
                    case "Uranium Rod Factory":
                        FactoryUraniumRod factoryUraniumRodDb = new();
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown factory name: {factory.Name}");
                };
            }
            factories = factoriesDB;

            GameLoop gameLoop = new(factories, factoryService, player, storageResources, energyStorage, mines, energyPlants);
            await Task.Delay(2000); // Simulate some initialization delay
            await gameLoop.StartAsync();
        }

        public static PlayerReal CreatePlayer()
        {
            byte lengthName = 5;
            string name = GenerateRandomName(lengthName);

            return new PlayerReal(name, 100000);
        }

        public static string GenerateRandomName(byte length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = chars[random.Next(chars.Length)];
            }

            return new string(buffer);
        }
    }
}
