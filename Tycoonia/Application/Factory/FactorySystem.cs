using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI;

namespace Tycoonia.Application.Factory
{
    public class FactorySystem
    {
        public static void ActionsFactory(List<FactoryBase> factories, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            for (int index = 1; (index - 1) < factories.Count; index++)
            {
                Console.WriteLine($"[{index}] {factories[index - 1].Name}");
            }
            Console.WriteLine("\nChoice number factory...");
            byte choiceNumberFactory = (byte)ConsoleInput.ConsoleChoice();
            FactoryBase currentFactory = factories[choiceNumberFactory - 1];

            FactoryInfo.ShowFactoryInfo(currentFactory);
            ChoiceFactoryActions(currentFactory, storageResources, energyStorage, player);
            
        }

        public static void ChoiceFactoryActions(FactoryBase currentFactory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            Console.WriteLine("\nChoose an actions:");
            Console.WriteLine("1. Start of production");
            Console.WriteLine("2. Stop of production");
            Console.WriteLine("3. Upgrade");
            Console.WriteLine("4. Exit");
            byte choiceActions = (byte)ConsoleInput.ConsoleChoice();

            switch (choiceActions)
            {
                case 1:
                    Console.WriteLine("Write ");
                    int choiceProductYield = (int)ConsoleInput.ConsoleChoice();

                    LaunchControleCenterFactory.PreparationLaunchFactory(currentFactory, storageResources, energyStorage, player, choiceProductYield);
                    UpdateFactoryCalculations(storageResources, currentFactory, energyStorage, player, choiceProductYield);
                    break;
                case 2:
                    LaunchControleCenterFactory.StopFactory(currentFactory, storageResources, player);
                    foreach (var item in storageResources.StorageList)
                    {
                        Console.WriteLine($"{item.Key}: {item.Value.CurrentQuantity}/{item.Value.MaxCapacity}");
                    }
                    Console.WriteLine("Production stopped.");
                    break;
                case 3:
                    try
                    {
                        UpgradeBuilding.Upgrade(currentFactory, storageResources, player);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    break;
                case 4:
                    Console.WriteLine("Exiting factory actions.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }






        public static void UpdateFactoryCalculations(StorageResources storageResources, FactoryBase currentFactory, EnergyStorage energyStorage, PlayerReal player, int choiceProductYield)
        {
            FactoryInfo.ShowFactoryInfo(currentFactory);

            int currentQuantity = 0;
            while (currentQuantity < choiceProductYield && currentFactory.WorkFlag)
            {
                currentQuantity += ProductionCalculation.ProductionCalculationFactory(storageResources, currentFactory, energyStorage);
                Console.WriteLine(currentQuantity);
                FactoryInfo.ShowFactoryInfo(currentFactory);
                foreach (var item in storageResources.StorageList)
                {
                    Console.WriteLine($"{item.Key}: {item.Value.CurrentQuantity}/{item.Value.MaxCapacity}");
                }
            }
            Console.WriteLine(currentQuantity);

            currentFactory.WorkFlag = false;
            currentFactory.ResourceBuffer.Clear();
            currentFactory.ProductionTime = 0;
        }
    }
}
