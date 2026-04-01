using Tycoonia.Application.Services;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.ProducedResources;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class SaveOfflineInStorage
    {
        public static async Task SaveOffline(FactoryService factoryService, StorageResources storageResources, Domain.Resources.Storage.EnergyStorage energyStorage, FactoryBase factory)
        {
            if (!factory.WorkFlag)
                return;

            DateTime now = DateTime.UtcNow;
            decimal seconds =(decimal)(now - factory.LastUpdateTime).TotalSeconds;
            decimal produced =factory.ProductionRate * seconds;
            decimal remaining =factory.TargetOutput - factory.Produced;

            if (produced > remaining)
                produced = remaining;

            foreach (var item in factory.ProductionItemList)
            {
                storageResources.AddResourceSafe(item.Key, (long)(item.Value * produced));
            }

            energyStorage.SubtractSafe(factory.EnergyConsumption * produced);

            factory.Produced += produced;

            factory.LastUpdateTime = now;

            if (factory.Produced >= factory.TargetOutput)
                factory.WorkFlag = false;

            await factoryService.UpdateFactory(factory);
        }
    }
}
