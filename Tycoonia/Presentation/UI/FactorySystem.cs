using Microsoft.Data.SqlClient;
using Tycoonia.Application;
using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Application.Factory;
using Tycoonia.Application.Services;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI.Info;

namespace Tycoonia.Presentation.UI
{
    public class FactorySystem
    {
        private static readonly SemaphoreSlim _dbSemaphore = new(1, 1);
        static FactoryBase currentFactory;

        public static async Task ActionsFactoryAsync(/*List<FactoryBase> factories*/ FactoryService factoryService, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            if (currentFactory != null)
            {
                await factoryService.UpdateFactory(currentFactory);
            }

            List<FactoryBase> factories = (await factoryService.GetAllFactoriesAsync()).ToList();

            for (int index = 1; (index - 1) < factories.Count; index++)
            {
                Console.WriteLine($"[{index}] {factories[index - 1].Name}");
            }
            Console.WriteLine("\nChoice number factory...");
            byte choiceNumberFactory = (byte)ConsoleInput.ConsoleChoice();
            currentFactory = factories[choiceNumberFactory - 1];

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
            await ChoiceFactoryActionsAsync(factoryService, currentFactory, storageResources, energyStorage, player);
            // Console.Clear();
        }

        public static async Task ChoiceFactoryActionsAsync(FactoryService factoryService, FactoryBase currentFactory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
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
                    await factoryService.UpdateFactory(currentFactory);
                    _ = UpdateFactoryCalculations(factoryService, storageResources, currentFactory, energyStorage, player);
                    break;
                case 2:
                    LaunchControleCenterFactory.StopFactory(currentFactory, storageResources, player);
                    StorageResourcesInfo.ShowStorageResourcesInfo(storageResources);
                    await factoryService.UpdateFactory(currentFactory);
                    Console.WriteLine("Production stopped.");
                    break;
                case 3:
                    try
                    {
                        UpgradeBuilding.Upgrade(currentFactory, storageResources, player);
                        await factoryService.UpdateFactory(currentFactory);
                        Console.WriteLine($"Upgrade completed | LVL {currentFactory.Level} |\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    break;
                case 4:
                    Console.WriteLine("Exiting factory actions.");
                    //await factoryService.UpdateFactory(currentFactory);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public async static Task UpdateFactoryCalculations(FactoryService factoryService, StorageResources storageResources, FactoryBase currentFactory, EnergyStorage energyStorage, PlayerReal player)
        {
            try
            {
                FactoryInfo.ShowFactoryInfo(currentFactory);
                while (currentFactory.ProductionTime > 0 && currentFactory.WorkFlag)
                {
                    ProductionCalculation.ProductionCalculationFactory(storageResources, currentFactory, energyStorage);
                    //await factoryService.UpdateFactory(currentFactory);
                    //await SafeUpdateFactory(factoryService, currentFactory);
                    //await _dbSemaphore.WaitAsync();
                    //try
                    //{
                    //    await SafeUpdateFactory(factoryService, currentFactory);
                    //}
                    //finally
                    //{
                    //    _dbSemaphore.Release();
                    //}
                    foreach (var item in currentFactory.ProductionItemList)
                    {
                        Console.WriteLine($"{item.Key}: {storageResources.StorageList[item.Key].CurrentQuantity}");
                    }
                    await Task.Delay(1000 /*+ Random.Shared.Next(0, 500)*/);
                }
                //Console.WriteLine("F clear1");
                //currentFactory.WorkFlag = false;
                //currentFactory.ResourceBuffer.Clear();
                //currentFactory.ProductionTime = 0m;
                //await factoryService.UpdateFactory(currentFactory);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[SYSTEM ERROR] Factory {currentFactory.Name} halted!");
                Console.WriteLine($"Reason: {ex.Message}");
                Console.WriteLine(ex.ToString());
                currentFactory.WorkFlag = false;
                foreach (var item in currentFactory.ResourceBuffer)
                {
                    item.Value.CurrentQuantity = 0;
                }
                currentFactory.ProductionTime = 0m;
                await factoryService.UpdateFactory(currentFactory);
            }
            finally
            {
                Console.WriteLine("Finaly stop factory");

                currentFactory.WorkFlag = false;
                currentFactory.ProductionTime = 0m;

                await factoryService.UpdateFactory(currentFactory);

                foreach (var item in currentFactory.ResourceBuffer)
                {
                    item.Value.CurrentQuantity = 0;
                }
            }
        }

        //public static async Task SafeUpdateFactory(FactoryService factoryService, FactoryBase factory)
        //{
        //    int retries = 3;

        //    for (int i = 0; i < retries; i++)
        //    {
        //        try
        //        {
        //            await factoryService.UpdateFactory(factory);
        //            return;
        //        }
        //        catch (SqlException ex) when (ex.Number == 1205)
        //        {
        //            Console.WriteLine($"Deadlock factory {factory.Name}, number1 {i + 1}");
        //            await Task.Delay(100);
        //        }
        //    }

        //    throw new Exception("deadlock");
        //}


    }
}
