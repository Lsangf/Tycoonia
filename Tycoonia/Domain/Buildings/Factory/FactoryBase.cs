using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Domain.Buildings.Factory
{
    public abstract class FactoryBase : GameEntityBase
    {
        private long _maxExpectedOtput;
        private decimal _energyConsumption;
        private decimal _productionTime;
        private decimal _productionTimePerIteration;
        private Dictionary<string, byte> _recipeList = [];
        private Dictionary<string, StorageResourcesBase> _resourceBuffer = [];
        private Dictionary<string, int> _productionItemList = [];
        private bool _cancelFlag;

        public long MaxExpectedOtput
        {
            get => _maxExpectedOtput;
            set => _maxExpectedOtput = value;
        }
        public decimal EnergyConsumption
        {
            get => _energyConsumption;
            set => _energyConsumption = value;
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
