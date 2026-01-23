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
                UpgradeCost = 10,
                CanUpgrade = false
            };
            StorageList["Coal"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false
            };
            StorageList["Uranium"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false
            };
            StorageList["Bricks"] = new StorageResourcesBase
            {
                CurrentQuantity = 1,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false
            };
        }
    }
}
