using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Domain.Buildings.EnergyPlant
{
    public abstract class EnergyPlantBase : GameEntityBase
    {
        private long _maxExpectedOtput;
        private decimal _productionTime;
        private decimal _productionTimePerIteration;
        private int _energyConsumption;
        private Dictionary<string, short> _recipeList = [];
        private Dictionary<string, StorageResourcesBase> _resourceBuffer = [];
        private Dictionary<string, int> _productionItemList = [];

        public long MaxExpectedOtput
        {
            get => _maxExpectedOtput;
            set => _maxExpectedOtput = value;
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
        public int EnergyConsumption
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
        public Dictionary<string, int> ProductionItemList
        {
            get => _productionItemList;
            set => _productionItemList = value;
        }

        public EnergyPlantBase()
        {
            Level = 1;
            CanUpgrade = false;
            WorkFlag = false;
        }
    }
}
