namespace Tycoonia.Domain.Resources.Storage
{
    public class EnergyStorage
    {
        private decimal _maxCapacity;
        private decimal _currentStorage;
        private bool _canUpgrade;
        private Dictionary<string, long> _receipeUpgradeList = new()
        {
            {"Money", 10 },
            {"Bricks", 1 } 
        };
        public decimal MaxCapacity
        {
            get => _maxCapacity;
            set => _maxCapacity = value;
        }
        public decimal CurrentStorage
        {
            get => _currentStorage;
            set => _currentStorage = value;
        }
        public bool CanUpgrade
        {
            get => _canUpgrade;
            set => _canUpgrade = value;
        }
        public Dictionary<string, long> ReceipeUpgradeList
        {
            get => _receipeUpgradeList;
            set => _receipeUpgradeList = value;
        }

        public EnergyStorage()
        {
            CanUpgrade = false;
            MaxCapacity = 100000m;
            CurrentStorage = 50000m;
        }

    }
}
