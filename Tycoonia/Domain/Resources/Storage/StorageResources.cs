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

        //    { "Clay", 10 },
        //    { "Coal", 5 },
        //    { "Uranium", 10 },
        //    { "Bricks", 0 }

        //StorageList = new Dictionary<string, Dictionary<string, long>>()
        //{
        //    {"Clay", new Dictionary<string, long>
        //        {
        //            {"Current quantity", 10 },
        //            {"Maximum Capacity", 100},
        //            {"Cost upgrade", 10 }
        //        }
        //},
        //    {"Coal", new Dictionary<string, long>
        //        {
        //            {"Current quantity", 10 },
        //            {"Maximum Capacity", 100},
        //            {"Cost upgrade", 10 }
        //        }
        //}
        //};
    }
}
