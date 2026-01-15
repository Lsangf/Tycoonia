namespace Tycoonia.Domain.Buildings.EnergyPlant.Storage
{
    public class EnergyStorage
    {
        private decimal _capacity;
        private decimal _currentStorage;
        public decimal Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }
        public decimal CurrentStorage
        {
            get => _currentStorage;
            set => _currentStorage = value;
        }
        public EnergyStorage()
        {
            Capacity = 100000m;
            CurrentStorage = 50000m;
        }

    }
}
