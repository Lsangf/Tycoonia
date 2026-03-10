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

            List<EnergyPlantBase> energyPlants =
            [
                coalTPP, fuelTPP, solidFuelTPP, uraniumNPP, thoriumNPP
            ];

            string connectionString = await SQLInitializer.InitializeAsync();
            DbConnectionProvider connectionProvider = new(connectionString);

            IRepository<FactoryBase> factoryRepository = new FactoryRepository(connectionProvider);

            DataInitializer dataInitializer = new(factoryRepository);
            await dataInitializer.Initialize();

            FactoryService factoryService = new(factoryRepository);

            GameLoop gameLoop = new(factoryService, player, storageResources, energyStorage, mines, energyPlants);
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
