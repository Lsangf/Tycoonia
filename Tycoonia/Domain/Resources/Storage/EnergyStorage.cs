namespace Tycoonia.Domain.Resources.Storage
{
    public class EnergyStorage
    {
        private decimal _maxCapacity;
        private decimal _currentStorage;
        private bool _canUpgrade;
        private short _level;
        private Dictionary<string, long> _recipeUpgradeList = new()
        {
            {"Money", 10 },
            {"Bricks", 1 } 
        };
        private readonly object _lock = new();

        public decimal MaxCapacity
        {
            get => _maxCapacity;
            set => _maxCapacity = value;
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
        public Dictionary<string, long> RecipeUpgradeList
        {
            get => _recipeUpgradeList;
            set => _recipeUpgradeList = value;
        }
        public decimal CurrentStorage
        {
            get { lock (_lock) return _currentStorage; }
            set { lock (_lock) _currentStorage = value; }
        }
        public void SubtractSafe(decimal amount)
        {
            lock (_lock)
            {
                if (_currentStorage < amount)
                    throw new Exception("Insufficient energy!");
                _currentStorage -= amount;
            }
        }
        public void AddSafe(long amount)
        {
            lock (_lock)
            {
                _currentStorage += amount;
                if (_currentStorage > MaxCapacity) _currentStorage = MaxCapacity;
            }
        }

        public EnergyStorage()
        {
            CanUpgrade = false;
            MaxCapacity = 100000m;
            CurrentStorage = 50000m;
            Level = 1;
        }
    }
}
