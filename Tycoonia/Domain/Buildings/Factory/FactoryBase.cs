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
        private Dictionary<string, long> _resourceBuffer = [];
        private Dictionary<string, int> _productionItemList = [];
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
        public Dictionary<string, long> ResourceBuffer
        {
            get => _resourceBuffer;
            set => _resourceBuffer = value;
        }
        public Dictionary<string, int> ProductionItemList
        {
            get => _productionItemList;
            set => _productionItemList = value;
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

        public FactoryBase()
        {
            Level = 1;
            CanUpgrade = false;
            WorkFlag = false;
            CancelFlag = false;
        }
    }
}
