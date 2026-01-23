namespace Tycoonia.Domain.Resources.Storage
{
    public class StorageResources
    {
        private bool _canUpgrade;
        private Dictionary<string, StorageResourcesBase> _storageList = [];

        public bool CanUpgrade
        {
            get => _canUpgrade;
            set => _canUpgrade = value;
        }
        public Dictionary<string, StorageResourcesBase> StorageList
        {
            get => _storageList;
            set => _storageList = value;
        }

        public StorageResources()
        {
            CanUpgrade = false;
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
