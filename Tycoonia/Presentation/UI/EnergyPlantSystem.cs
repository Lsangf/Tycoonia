using Tycoonia.Application;
using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Application.Energy;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI.Info;

namespace Tycoonia.Presentation.UI
{
    public class EnergyPlantSystem
    {
        public static void ActionsEnergyPlant(List<EnergyPlantBase> energyPlants, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            for (int index = 1; (index - 1) < energyPlants.Count; index++)
            {
                Console.WriteLine($"[{index}] {energyPlants[index - 1].Name}");
            }
            Console.WriteLine("\nChoice number factory...");
            byte choiceNumberEnergyPlant = (byte)ConsoleInput.ConsoleChoice();
            EnergyPlantBase currentEnergyPlant = energyPlants[choiceNumberEnergyPlant - 1];

            EnergyPlantInfo.ShowEnergyPlantInfo(currentEnergyPlant);
            Console.WriteLine("| Current |");
            foreach (var item in currentEnergyPlant.RecipeList)
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
            MaximumPossibleExpectedOtput.UpdateMaximumPossibleExpectedOtput(currentEnergyPlant, storageResources, player);
            Console.WriteLine($"Max exception output: {currentEnergyPlant.MaxExpectedOtput}");
            ChoiceEnergyPlantActions(currentEnergyPlant, storageResources, energyStorage, player);
        }

        public static void ChoiceEnergyPlantActions(EnergyPlantBase currentEnergyPlant, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            Console.WriteLine("\nChoose an actions:");
            Console.WriteLine("1. Start of power generation (Warning! Disconnection in progress is unavailable.)");
            Console.WriteLine("2. Upgrade");
            Console.WriteLine("3. Exit");
            byte choiceActions = (byte)ConsoleInput.ConsoleChoice();

            switch (choiceActions)
            {
                case 1:
                    Console.WriteLine("Write ");
                    int choiceProductYield = (int)ConsoleInput.ConsoleChoice();
                    if (choiceProductYield < currentEnergyPlant.ProductionRate)
                    {
                        throw new InputException();
                    }

                    LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(currentEnergyPlant, storageResources, energyStorage, player, choiceProductYield);
                    _ = UpdateEnergyPlantCalculations(storageResources, currentEnergyPlant, energyStorage, player, choiceProductYield);
                    break;
                case 2:
                    try
                    {
                        UpgradeBuilding.Upgrade(currentEnergyPlant, storageResources, player);
                        Console.WriteLine($"Upgrade completed | LVL {currentEnergyPlant.Level} |\n");
                    }
                    catch (Exception ex) { Console.WriteLine($"An error occurred: {ex.Message}"); }
                    break;
                case 3:
                    Console.WriteLine("Exiting energy plant actions.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }

        public async static Task UpdateEnergyPlantCalculations(StorageResources storageResources, EnergyPlantBase currentEnergyPlant, EnergyStorage energyStorage, PlayerReal player, int choiceProductYield)
        {
            try
            {
                EnergyPlantInfo.ShowEnergyPlantInfo(currentEnergyPlant);

                byte iteration = 0;
                Dictionary<string, int> currentEnergyPlantResult = [];
                while (currentEnergyPlant.ProductionTime > 0 && currentEnergyPlant.WorkFlag)
                {
                    if (iteration == 1)
                    {
                        EnergeticsCalculation.EnergeticsCalculationEnergyPlant(storageResources, currentEnergyPlant, energyStorage, iteration);
                        await Task.Delay(6000);
                    }
                    else
                    {
                        EnergeticsCalculation.EnergeticsCalculationEnergyPlant(storageResources, currentEnergyPlant, energyStorage, iteration);
                        iteration++;
                        await Task.Delay(6000);
                    }
                    Console.WriteLine($"Current Storage Resources: {storageResources.StorageList["Plutonium-239"]}");
                    EnergyPlantInfo.ShowEnergyPlantInfo(currentEnergyPlant);
                    Console.WriteLine($"Current energy: {energyStorage.CurrentStorage}");
                    StorageResourcesInfo.ShowStorageResourcesInfo(storageResources);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[SYSTEM ERROR] Factory {currentEnergyPlant.Name} halted!");
                Console.WriteLine($"Reason: {ex.Message}");
                currentEnergyPlant.WorkFlag = false;
            }
            finally
            {
                currentEnergyPlant.WorkFlag = false;
                currentEnergyPlant.ResourceBuffer.Clear();
                currentEnergyPlant.ProductionTime = 0m;
            }
        }
    }
}
