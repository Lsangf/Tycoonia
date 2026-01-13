namespace Tycoonia.Domain.Buildings.Mine
{
    internal abstract class MineBase
    {
        private string _name;
        private short _level;
        private bool _canUpgrade;
        private int _productionRate;
        private float _energyConsumption;
        private byte _state;
        private (int, int) _position;

        public string Name
        {
            get => _name;
            protected set => _name = value;
        }
        public short Level
        {
            get => _level;
            set => _level = value;
        }
        public bool CanUpgrade
        {
            get => _canUpgrade;
            set => _canUpgrade = value;
        }
        public int ProductionRate
        {
            get => _productionRate;
            set => _productionRate = value;
        }
        public float EnergyConsumption
        {
            get => _energyConsumption;
            set => _energyConsumption = value;
        }
        public byte State
        {
            get => _state;
            set => _state = value;
        }
        public (int, int) Position
        {
            get => _position;
            set => _position = value;
        }

        protected MineBase((int, int) position)
        {
            Level = 1;
            CanUpgrade = false;
            State = 100;
            Position = position;
        }
    }
}
