using Tycoonia.Application.Services;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class SaveOfflineInStorage
    {
        public static async Task SaveOffline(FactoryService factoryService, StorageResources storageResources, FactoryBase factory)
        {
            decimal amountIterations = factory.ProductionTime * factory.ProductionTimePerIteration;
            amountIterations = Math.Ceiling(amountIterations);
            foreach (var item in factory.ProductionItemList)
            {
                storageResources.AddResourceSafe(item.Key, (long)Math.Ceiling(item.Value * amountIterations));
            }
            factory.ProductionTime = 0m;
            factory.ResourceBuffer.Clear();
            factory.WorkFlag = false;

            await factoryService.UpdateFactory(factory);
        }
    }
}
