namespace Tycoonia.Domain.Buildings.Mine
{
    public abstract class MineBase : GameEntityBase
    {
        private short _MAXLevel;
        private decimal _energyConsumption;
        private byte _state;
        private (int, int) _position;
        private string _productionItem;
        private bool _cancelFlag;

        public short MAXLevel
        {
            get => _MAXLevel;
            protected set => _MAXLevel = value;
        }
        public decimal EnergyConsumption
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
        public string ProductionItem
        {
            get => _productionItem;
            set => _productionItem = value;
        }
        public bool CancelFlag
        {
            get => _cancelFlag;
            set => _cancelFlag = value;
        }

        public MineBase((int, int) position)
        {
            Level = 1;
            MAXLevel = 5;
            CanUpgrade = false;
            State = 100;
            Position = position;
            WorkFlag = false;
            CancelFlag = false;
        }
    }
}
