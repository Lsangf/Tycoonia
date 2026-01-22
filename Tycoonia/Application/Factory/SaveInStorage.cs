using Tycoonia.Application.ApplicationExceptions;
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
                if (storageResources.StorageList.ContainsKey(item.Key))
                {
                    storageResources.StorageList[item.Key] += item.Value;
                }
                else
                {
                    throw new StorageException();
                }
            }
        }
    }
}
