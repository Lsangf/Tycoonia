using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Presentation.UI
{
    public class ConsoleChoiceSystem
    {
        public static byte ConsoleChoice(
            List<MineBase> mines, 
            List<FactoryBase> factories,
            List<EnergyPlantBase> energyPlants, 
            StorageResources storageResources, EnergyStorage energyStorage)
        {
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Manage Mines");
                Console.WriteLine("2. Manage Factories");
                Console.WriteLine("3. Manage Energy Plants");
                Console.WriteLine("4. View Resources");
                Console.WriteLine("5. View Energy");
                Console.WriteLine("6. Exit");
                byte choice = (byte)ConsoleInput.ConsoleChoice();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nManaging Mines...");
                        if (mines == null)
                        {
                            //MineClay mineClay = new((0, 0));
                            //MineInfo.ShowMineInfo(mineClay);
                            Console.WriteLine("No mines available.");
                            break;
                        }
                        else
                        {
                            // MineSystem.
                        }
                        break;
                    case 2:
                        Console.WriteLine("\nManaging Factories...");

                        // FactorySystem.
                        break;
                    case 3:
                        Console.WriteLine("\nManaging Energy Plants...");

                        // EnergyPlantSystem.
                        break;
                    case 4:
                        Console.WriteLine("\nViewing Resources...");

                        // StorageResourcesSystem.
                        break;
                    case 5:
                        Console.WriteLine("\nViewing Energy...");

                        // EnergyStorageSystem.
                        break;
                    case 6:
                        Console.WriteLine("\nExiting...");

                        // Exeiting.
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }
    }
}
