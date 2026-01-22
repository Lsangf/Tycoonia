namespace Tycoonia.Application.Storage
{
    public class StorageResourcesBase
    {
        private long _currentQuantity;
        private long _maxCapacity;
        private long _upgradeCost;

        public long CurrentQuantity
        {
            get => _currentQuantity;
            set => _currentQuantity = value;
        }
        public long MaxCapacity
        {
            get => _maxCapacity;
            set => _maxCapacity = value;
        }
        public long UpgradeCost
        {
            get => _upgradeCost;
            set => _upgradeCost = value;
        }
    }
}
