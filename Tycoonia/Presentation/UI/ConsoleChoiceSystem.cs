using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Presentation.UI
{
    public class ConsoleChoiceSystem
    {
        public static void ConsoleChoice(
            List<MineBase> mines,
            PlayerReal player,
            List<FactoryBase> factories,
            List<EnergyPlantBase> energyPlants,
            StorageResources storageResources, EnergyStorage energyStorage)
        {
            bool _isRunning = true;
            while (_isRunning)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. View Energy");
                Console.WriteLine("2. Manage Mines");
                Console.WriteLine("3. Manage Factories");
                Console.WriteLine("4. Manage Energy Plants");
                Console.WriteLine("5. View Resources");
                Console.WriteLine("6. View Energy");
                Console.WriteLine("7. Manage Market");
                Console.WriteLine("8. View Profile");
                Console.WriteLine("9. Exit");
                try
                {
                    byte choice = (byte)ConsoleInput.ConsoleChoice();

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\nManaging Map...");
                            // MapSystem.ActionsMap(mines, storageResources, energyStorage, player);
                            break;
                        case 2:
                            Console.WriteLine("\nManaging Mines...");
                            MineSystem.ActionsMine(mines, storageResources, energyStorage, player);
                            break;
                        case 3:
                            Console.WriteLine("\nManaging Factories...");
                            FactorySystem.ActionsFactory(factories, storageResources, energyStorage, player);
                            break;
                        case 4:
                            Console.WriteLine("\nManaging Energy Plants...");
                            EnergyPlantSystem.ActionsEnergyPlant(energyPlants, storageResources, energyStorage, player);
                            break;
                        case 5:
                            Console.WriteLine("\nViewing Resources...");
                            StorageResourcesSystem.ActionsStorageResources(storageResources, player);
                            break;
                        case 6:
                            Console.WriteLine("\nViewing Energy...");
                            EnergyStorageSystem.ActionsEnergyStorage(energyStorage, storageResources, player);
                            break;
                        case 7:
                            Console.WriteLine("\nManaging Market...");
                            MineSystem.ActionsMine(mines, storageResources, energyStorage, player);
                            break;
                        case 8:
                            Console.WriteLine("\nViewing Profile...");
                            MineSystem.ActionsMine(mines, storageResources, energyStorage, player);
                            break;
                        case 9:
                            Console.WriteLine("\nExiting...");
                            _isRunning = false;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid choice. Please try again.\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"{ex.Message}\n");
                }
            }
        }
    }
}
