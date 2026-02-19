using Tycoonia.Application;
using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Application.Factory;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI.Info;

namespace Tycoonia.Presentation.UI
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
            Console.WriteLine("| Current |");
            foreach (var item in currentFactory.RecipeList)
            {
                if (item.Key == "Money")
                {
                    Console.WriteLine($"{item.Key}: {player.Ballance}");
                }
                else
                {
                    Console.WriteLine($"{item.Key}: {storageResources.StorageList[item.Key].CurrentQuantity}");
                }
            }
            MaximumPossibleExpectedOtput.UpdateMaximumPossibleExpectedOtput(currentFactory, storageResources, player);
            Console.WriteLine($"Max exception output: {currentFactory.MaxExpectedOtput}");
            ChoiceFactoryActions(currentFactory, storageResources, energyStorage, player);
            // Console.Clear();
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
                    if (currentFactory.WorkFlag)
                    {
                        Console.WriteLine("Factory is already working.");
                        break;
                    }
                    Console.WriteLine("Write ");
                    int choiceProductYield = (int)ConsoleInput.ConsoleChoice();
                    if (choiceProductYield < currentFactory.ProductionRate) 
                    {
                        throw new InputException();
                    }
                    LaunchControleCenterFactory.PreparationLaunchFactory(currentFactory, storageResources, energyStorage, player, choiceProductYield);
                    _ = UpdateFactoryCalculations(storageResources, currentFactory, energyStorage, player);
                    break;
                case 2:
                    LaunchControleCenterFactory.StopFactory(currentFactory, storageResources, player);
                    StorageResourcesInfo.ShowStorageResourcesInfo(storageResources);
                    Console.WriteLine("Production stopped.");
                    break;
                case 3:
                    try
                    {
                        UpgradeBuilding.Upgrade(currentFactory, storageResources, player);
                        Console.WriteLine($"Upgrade completed | LVL {currentFactory.Level} |\n");
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

        public async static Task UpdateFactoryCalculations(StorageResources storageResources, FactoryBase currentFactory, EnergyStorage energyStorage, PlayerReal player)
        {
            try
            {
                FactoryInfo.ShowFactoryInfo(currentFactory);
                while (currentFactory.ProductionTime > 0 && currentFactory.WorkFlag)
                {
                    ProductionCalculation.ProductionCalculationFactory(storageResources, currentFactory, energyStorage);
                    await Task.Delay(6000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[SYSTEM ERROR] Factory {currentFactory.Name} halted!");
                Console.WriteLine($"Reason: {ex.Message}");
                currentFactory.WorkFlag = false;
            }
            finally
            {
                currentFactory.WorkFlag = false;
                currentFactory.ResourceBuffer.Clear();
                currentFactory.ProductionTime = 0m;
            }
        }
    }
}
