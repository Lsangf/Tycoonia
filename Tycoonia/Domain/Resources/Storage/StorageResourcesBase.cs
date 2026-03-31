using Tycoonia.Application.ApplicationExceptions;

namespace Tycoonia.Domain.Resources.Storage
{
    public class StorageResourcesBase
    {
        private decimal _currentQuantity;
        private decimal _maxCapacity;
        private long _upgradeCost;
        private bool _canUpgrade;
        private short _level;
        private int _price;
        private readonly object _lock = new();

        public decimal CurrentQuantity
        {
            get { lock (_lock) return _currentQuantity; }
            set { lock (_lock) _currentQuantity = value; }
        }
        public decimal MaxCapacity
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
        public int Price
        {
            get => _price;
            set => _price = value;
        }
        public void Add(decimal amount)
        {
            lock (_lock)
            {
                _currentQuantity += amount;
                if (_currentQuantity > MaxCapacity) _currentQuantity = MaxCapacity;
            }
        }
        public void Subtract(decimal amount)
        {
            lock (_lock)
            {
                if (_currentQuantity < amount)
                    throw new StorageException("Not enough resources in storage!");
                _currentQuantity -= amount;
            }
        }
    }
}
