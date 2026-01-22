namespace Tycoonia.Domain.Buildings.EnergyPlant.Storage
{
    public class EnergyStorage
    {
        private decimal _maxCapacity;
        private decimal _currentStorage;
        private Dictionary<string, long> _receipeUpgradeList = new()
        {
            {"?", 1000 } 
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
        public Dictionary<string, long> ReceipeUpgradeList
        {
            get => _receipeUpgradeList;
            set => _receipeUpgradeList = value;
        }

        public EnergyStorage()
        {
            MaxCapacity = 100000m;
            CurrentStorage = 50000m;
        }

    }
}
