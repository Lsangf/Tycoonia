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
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Coal"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Uranium"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Bricks"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
        }
    }
}
