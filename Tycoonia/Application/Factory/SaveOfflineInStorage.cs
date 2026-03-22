using Tycoonia.Application.Services;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class SaveOfflineInStorage
    {
        public static async Task SaveOffline(FactoryService factoryService, StorageResources storageResources, FactoryBase factory)
        {
            decimal amountIterations = Math.Ceiling(factory.ProductionTime / factory.ProductionTimePerIteration);

            foreach (var item in factory.ProductionItemList)
            {
                storageResources.AddResourceSafe(item.Key, (long)(item.Value * amountIterations));
            }
            foreach (var item in factory.ResourceBuffer)
            {
                item.Value.CurrentQuantity = 0;
            }

            factory.ProductionTime = 0m;
            //factory.ResourceBuffer.Clear();
            factory.WorkFlag = false;

            await factoryService.UpdateFactory(factory);
        }

        public static async Task SaveOfflinePartially(FactoryService factoryService, StorageResources storageResources, EnergyStorage energyStorage, FactoryBase factory, decimal differenceSeconds)
        {
            decimal amountIterations = Math.Ceiling(factory.ProductionTime / factory.ProductionTimePerIteration);
            decimal amountOfflineIterations = amountIterations - differenceSeconds;

            foreach (var itemProduction in factory.ProductionItemList)
            {
                storageResources.AddResourceSafe(itemProduction.Key, (long)(itemProduction.Value * amountOfflineIterations));

            }
            foreach (var item in factory.ResourceBuffer)
            {
                foreach (var itemProduction in factory.ProductionItemList)
                {
                    if (item.Key == itemProduction.Key)
                    {
                        storageResources.AddResourceSafe(item.Key, (long)(item.Value.CurrentQuantity - (itemProduction.Value * amountOfflineIterations)));
                    }
                }
            }

            factory.ProductionTime -= factory.ProductionTimePerIteration * amountOfflineIterations;
            energyStorage.SubtractSafe(factory.EnergyConsumption * amountOfflineIterations);
            await factoryService.UpdateFactory(factory);
        }
    }
}
