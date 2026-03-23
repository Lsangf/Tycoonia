using Tycoonia.Application.Services;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class SaveOfflineInStorage
    {
        public static async Task SaveOffline(FactoryService factoryService, StorageResources storageResources, FactoryBase factory)
        {
            decimal amountIterations = factory.ProductionTime;

            foreach (var item in factory.ProductionItemList)
            {
                storageResources.AddResourceSafe(item.Key, (long)(item.Value * amountIterations));
            }
            foreach (var item in factory.ResourceBuffer)
            {
                item.Value.CurrentQuantity = 0;
            }

            factory.ProductionTime = 0m;
            factory.ProgressTime = TimeSpan.Zero;
            factory.WorkFlag = false;

            await factoryService.UpdateFactory(factory);
        }

        public static async Task SaveOfflinePartially(FactoryService factoryService, StorageResources storageResources, EnergyStorage energyStorage, FactoryBase factory, decimal differenceSeconds)
        {
            decimal amountIterations = factory.ProductionTime;
            decimal amountOfflineIterations = amountIterations - differenceSeconds;

            foreach (var itemProduction in factory.ProductionItemList)
            {
                storageResources.AddResourceSafe(itemProduction.Key, (long)(itemProduction.Value * amountOfflineIterations));
            }

            foreach (var item in factory.ResourceBuffer)
            {
                foreach (var itemProduction in factory.RecipeList)
                {
                    if (item.Key == itemProduction.Key)
                    {
                        factory.ResourceBuffer[item.Key].CurrentQuantity -= (long)(itemProduction.Value * amountOfflineIterations);
                    }
                }
            }

            factory.ProgressTime -= TimeSpan.FromSeconds(Math.Floor((double)differenceSeconds));
            energyStorage.SubtractSafe(factory.EnergyConsumption * amountOfflineIterations);
            await factoryService.UpdateFactory(factory);
        }
    }
}
