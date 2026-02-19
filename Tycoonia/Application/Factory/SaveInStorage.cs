using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class SaveInStorage
    {
        public static void Save(StorageResources storageResources, FactoryBase factory)
        {
            foreach (var item in factory.ProductionItemList)
            {
                storageResources.AddResourceSafe(item.Key, item.Value);
            }
        }
    }
}
