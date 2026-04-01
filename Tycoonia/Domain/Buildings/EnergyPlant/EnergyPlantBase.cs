using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Domain.Buildings.EnergyPlant
{
    public abstract class EnergyPlantBase : GameEntityBase, IUpgradableBuilding
    {
        private long _maxExpectedOtput;
        private decimal _productionTime;
        private TimeSpan _progressTime;
        private decimal _energyConsumption;
        private Dictionary<string, short> _recipeList = [];
        private Dictionary<string, StorageResourcesBase> _resourceBuffer = [];
        private Dictionary<string, decimal> _productionItemList = [];
        private decimal _targetOutput;
        private decimal _produced;
        private DateTime _lastUpdateTime;

        public long MaxExpectedOtput
        {
            get => _maxExpectedOtput;
            set => _maxExpectedOtput = value;
        }
        public DateTime LastUpdateTime
        {
            get => _lastUpdateTime;
            set => _lastUpdateTime = value;
        }
        public decimal ProductionTime
        {
            get => _productionTime;
            set => _productionTime = value;
        }
        public TimeSpan ProgressTime
        {
            get => _progressTime;
            set => _progressTime = value;
        }
        public decimal EnergyConsumption
        {
            get => _energyConsumption;
            set => _energyConsumption = value;
        }
        public Dictionary<string, short> RecipeList
        {
            get => _recipeList;
            set => _recipeList = value;
        }
        public Dictionary<string, StorageResourcesBase> ResourceBuffer
        {
            get => _resourceBuffer;
            set => _resourceBuffer = value;
        }
        public Dictionary<string, decimal> ProductionItemList
        {
            get => _productionItemList;
            set => _productionItemList = value;
        }
        public decimal TargetOutput
        {
            get => _targetOutput;
            set => _targetOutput = value;
        }
        public decimal Produced
        {
            get => _produced;
            set => _produced = value;
        }

        public EnergyPlantBase()
        {
            Level = 1;
            CanUpgrade = false;
            WorkFlag = false;
        }
    }
}
