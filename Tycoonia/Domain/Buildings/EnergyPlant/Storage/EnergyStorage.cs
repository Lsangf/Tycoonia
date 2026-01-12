namespace Tycoonia.Domain.Buildings.EnergyPlant.Storage
{
    internal class EnergyStorage
    {
        private long _capacity;
        private long _currentStorage;
        public long Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }
        public long CurrentStorage
        {
            get => _currentStorage;
            set => _currentStorage = value;
        }
        public EnergyStorage()
        {
            Capacity = 1000;
            CurrentStorage = 0;
        }

    }
}
