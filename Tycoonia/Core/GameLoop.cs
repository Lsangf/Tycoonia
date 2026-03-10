using Tycoonia.Application.Services;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI;

namespace Tycoonia.Core
{
    public class GameLoop
    {
        private readonly FactoryService _factoryService;
        private PlayerReal _player;
        private StorageResources _storageResources;
        private EnergyStorage _energyStorage;
        private List<MineBase> _mines;
        private List<EnergyPlantBase> _energyPlants;

        public GameLoop(FactoryService factoryService, PlayerReal player, StorageResources storageResources, EnergyStorage energyStorage, List<MineBase> mines, List<EnergyPlantBase> energyPlants)
        {
            _factoryService = factoryService;
            _player = player;
            _storageResources = storageResources;
            _energyStorage = energyStorage;
            _mines = mines;
            _energyPlants = energyPlants;
        }

        public async Task StartAsync()
        {
            ConsoleChoiceSystem.ConsoleChoice(_mines, _player, (List<FactoryBase>)await _factoryService.GetAllFactoriesAsync(), _energyPlants, _storageResources, _energyStorage);

            //bool running = true;

            //while (running)
            //{
            //    Console.Clear();

            //    Console.WriteLine("=== FACTORIES ===");

            //   var factories = await _factoryService.GetAllFactoriesAsync();

            //    foreach (var factory in factories)
            //    {
            //        Console.WriteLine($"{factory.Id} | {factory.Name} | Level: {factory.Level}");
            //    }

            //    Console.WriteLine();
            //    Console.WriteLine("1 - Upgrade factory (Soon(local))");
            //    Console.WriteLine("2 - Exit");

            //    string input = Console.ReadLine();

            //    switch (input)
            //    {
            //        case "1":
            //            await UpgradeFactoryAsync();
            //            break;
            //        case "2":
            //            running = false;
            //            break;
            //    }
            //}
        }

        private async Task UpgradeFactoryAsync()
        {
            Console.WriteLine("Enter factory id:");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var factory = await _factoryService.GetByIdFactory(id);
                factory.Level+=2;
                factory.ProductionRate+=5;
                await _factoryService.UpdateFactory(factory);
            }
        }
    }
}
