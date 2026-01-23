using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Domain.Buildings.EnergyPlant
{
    public abstract class EnergyPlantBase
    {
        private string _name;
        private short _level;
        private bool _canUpgrade;
        private Dictionary<string, long> _receipeUpgradeList = [];
        private int _productionRate;
        private int _productionTime;
        private int energyConsumption;
        private Dictionary<string, short> _receipeList = [];
        private Dictionary<string, StorageResourcesBase> _resourceBuffer = [];
        private Dictionary<string, int> _productionItemList = [];
        private bool _workFlag;

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
        public int ProductionTime
        {
            get => _productionTime;
            set => _productionTime = value;
        }
        public int EnergyConsumption
        {
            get => energyConsumption;
            set => energyConsumption = value;
        }
        public Dictionary<string, short> ReceipeList
        {
            get => _receipeList;
            set => _receipeList = value;
        }
        public Dictionary<string, StorageResourcesBase> ResourceBuffer
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

        public EnergyPlantBase()
        {
            Level = 1;
            CanUpgrade = false;
            WorkFlag = false;
        }
    }
}
