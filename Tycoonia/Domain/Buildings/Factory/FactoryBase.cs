using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Domain.Buildings.Factory
{
    public abstract class FactoryBase
    {
        private string _name;
        private short _level;
        private bool _canUpgrade;
        private Dictionary<string, long> _recipeUpgradeList = [];
        private int _productionRate;
        private long _maxExpectedOtput;
        private decimal energyConsumption;
        private decimal _productionTime;
        private decimal _productionTimePerIteration;
        private Dictionary<string, byte> _recipeList = [];
        private Dictionary<string, StorageResourcesBase> _resourceBuffer = [];
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
        public Dictionary<string, long> RecipeUpgradeList
        {
            get => _recipeUpgradeList;
            set => _recipeUpgradeList = value;
        }
        public int ProductionRate
        {
            get => _productionRate;
            set => _productionRate = value;
        }
        public long MaxExpectedOtput
        {
            get => _maxExpectedOtput;
            set => _maxExpectedOtput = value;
        }
        public decimal EnergyConsumption
        {
            get => energyConsumption;
            set => energyConsumption = value;
        }
        public decimal ProductionTime
        {
            get => _productionTime;
            set => _productionTime = value;
        }
        public decimal ProductionTimePerIteration
        {
            get => _productionTimePerIteration;
            set => _productionTimePerIteration = value;
        }
        public Dictionary<string, byte> RecipeList
        {
            get => _recipeList;
            set => _recipeList = value;
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
