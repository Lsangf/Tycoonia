namespace Tycoonia.Domain.Buildings.Mine
{
    public abstract class MineBase
    {
        private string _name;
        private short _level;
        private short _MAXLevel;
        private bool _canUpgrade;
        private Dictionary<string, long> _recipeUpgradeList = [];
        private decimal _energyConsumption;
        private byte _state;
        private (int, int) _position;
        private int _productionRate;
        private string _productionItem;
        private bool _workFlag;
        private bool _cancelFlag;

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
        public short MAXLevel
        {
            get => _MAXLevel;
            protected set => _MAXLevel = value;
        }
        public bool CanUpgrade
        {
            get => _canUpgrade;
            set => _canUpgrade = value;
        }
        public Dictionary<string, long> RecipeUpgradeList
        {
            get => _recipeUpgradeList;
            set => _recipeUpgradeList = value;
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
        public int ProductionRate
        {
            get => _productionRate;
            set => _productionRate = value;
        }
        public string ProductionItem
        {
            get => _productionItem;
            set => _productionItem = value;
        }
        public bool WorkFlag
        {
            get => _workFlag;
            set => _workFlag = value;
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
