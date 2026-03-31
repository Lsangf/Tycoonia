using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Domain.Buildings.EnergyPlant
{
    public abstract class EnergyPlantBase : GameEntityBase, IUpgradableBuilding
    {
        private long _maxExpectedOtput;
        //private DateTime _timeStart;
        //private DateTime _timeEnd;
        private decimal _productionTime;
        private TimeSpan _progressTime;
        private decimal _energyConsumption;
        private Dictionary<string, short> _recipeList = [];
        private Dictionary<string, StorageResourcesBase> _resourceBuffer = [];
        private Dictionary<string, decimal> _productionItemList = [];

        public long MaxExpectedOtput
        {
            get => _maxExpectedOtput;
            set => _maxExpectedOtput = value;
        }
        //public DateTime TimeStart
        //{
        //    get => _timeStart;
        //    set => _timeStart = value;
        //}
        //public DateTime TimeEnd
        //{
        //    get => _timeEnd;
        //    set => _timeEnd = value;
        //}
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
        //public decimal ProductionTimePerIteration
        //{
        //    get => _productionTimePerIteration;
        //    set => _productionTimePerIteration = value;
        //}
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
        public decimal ProductionTimePerIteration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan ProgressTimeUi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public EnergyPlantBase()
        {
            Level = 1;
            CanUpgrade = false;
            WorkFlag = false;
        }
    }
}
