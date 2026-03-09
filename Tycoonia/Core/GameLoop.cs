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

        public GameLoop(FactoryService factoryService)
        {
            _factoryService = factoryService;
        }

        public async Task StartAsync()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("=== FACTORIES ===");

                var factories = _factoryService.GetAllFactoriesAsync().GetAwaiter().GetResult();

                foreach (var factory in factories)
                {
                    Console.WriteLine($"{factory.Id} | {factory.Name} | Level: {factory.Level}");
                }

                Console.WriteLine();
                Console.WriteLine("1 - Upgrade factory");
                Console.WriteLine("2 - Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await UpgradeFactoryAsync();
                        break;

                    case "2":
                        running = false;
                        break;
                }
            }
        }

        private async Task UpgradeFactoryAsync()
        {
            Console.WriteLine("Enter factory id:");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _factoryService.UpgradeFactory(id);
            }
        }
    }
}
