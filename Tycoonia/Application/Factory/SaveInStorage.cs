using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class SaveInStorage
    {
        public static void Save(StorageResources storageResources, FactoryBase factory, int fabrication)
        {
            if (storageResources.Storage.ContainsKey(factory.ProductionItem))
            {
                storageResources.Storage[factory.ProductionItem] += fabrication;
            }
            else
            {
                throw new Exception("Resource type not found in storage.");
            }
        }
    }
}
