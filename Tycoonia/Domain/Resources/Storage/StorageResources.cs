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
            StorageList["Water"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Clay"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Gravel"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Quartz Sand"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Limestone"] = new StorageResourcesBase
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
            StorageList["Iron"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Copper"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Silver"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Gold"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Bauxite"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Oil"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Lithium"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Limenite"] = new StorageResourcesBase
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
            StorageList["Thorium-232"] = new StorageResourcesBase
            {
                CurrentQuantity = 10,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Rough Diamonds"] = new StorageResourcesBase
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
            StorageList["Concrete"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Steel"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Copper Wire"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Silver Bars"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Gold Bars"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Aluminum"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Titanium"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Glass"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Silicon"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Electronic Components"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Plastic"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Fuel"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Solid Fuel"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Purified Lithium"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Batteries"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Energy Storage"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Diamonds"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Uranium-238"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Uranium-235"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Uranium Rod"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Plutonium-239"] = new StorageResourcesBase
            {
                CurrentQuantity = 50,
                MaxCapacity = 100,
                UpgradeCost = 10,
                CanUpgrade = false,
                Level = 1
            };
            StorageList["Thorium Rod"] = new StorageResourcesBase
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
