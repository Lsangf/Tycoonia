namespace Tycoonia.Domain.Resources.Storage
{
    public class StorageResourcesBase
    {
        private long _currentQuantity;
        private long _maxCapacity;
        private long _upgradeCost;
        private bool _canUpgrade;
        private short _level;

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
        public bool CanUpgrade
        {
            get => _canUpgrade;
            set => _canUpgrade = value;
        }
        public short Level
        {
            get => _level;
            set => _level = value;
        }
    }
}
