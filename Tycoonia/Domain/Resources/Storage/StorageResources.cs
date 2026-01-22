using Tycoonia.Application.Storage;

namespace Tycoonia.Domain.Resources.Storage
{
    public class StorageResources
    {
        private Dictionary<string, StorageResourcesBase> _storageList = [];

        public Dictionary<string, StorageResourcesBase> StorageList
        {
            get => _storageList;
            set => _storageList = value;
        }

        public StorageResources()
        {
            StorageList["Clay"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10
            };
            StorageList["Coal"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10
            };
            StorageList["Uranium"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10
            };
            StorageList["Bricks"] = new StorageResourcesBase
            {
                CurrentQuantity = 0,
                MaxCapacity = 100,
                UpgradeCost = 10
            };
        }
    }
}
