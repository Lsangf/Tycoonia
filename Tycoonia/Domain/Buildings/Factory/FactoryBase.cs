namespace Tycoonia.Domain.Buildings.Factory
{
    public abstract class FactoryBase
    {
        private string _name;
        private short _level;
        private bool _canUpgrade;
        private Dictionary<string, long> _receipeUpgradeList = [];
        private int _productionRate;
        private decimal energyConsumption;
        private int _productionTime;
        private Dictionary<string, byte> _receipeList = [];
        private string _productionItem;
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
        public int ProductionRate
        {
            get => _productionRate;
            set => _productionRate = value;
        }
        public decimal EnergyConsumption
        {
            get => energyConsumption;
            set => energyConsumption = value;
        }
        public int ProductionTime
        {
            get => _productionTime;
            set => _productionTime = value;
        }
        public Dictionary<string, byte> ReceipeList
        {
            get => _receipeList;
            set => _receipeList = value;
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

        public FactoryBase()
        {
            Level = 1;
            CanUpgrade = false;
            CancelFlag = false;
        }
    }
}
