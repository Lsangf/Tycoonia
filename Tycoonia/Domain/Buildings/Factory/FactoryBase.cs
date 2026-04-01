using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Domain.Buildings.Factory
{
    public abstract class FactoryBase : GameEntityBase, IUpgradableBuilding
    {
        private decimal _maxExpectedOtput;
        private decimal _energyConsumption;
        private DateTime _lastUpdateTime;
        //private DateTime _timeStart;
        //private DateTime _timeEnd;
        //private decimal _productionTime;
        //private TimeSpan _progressTime;
        //private TimeSpan _progressTimeUi;
        //private decimal _productionTimePerIteration;
        private decimal _targetOutput;
        private decimal _produced;
        private Dictionary<string, long> _recipeList = [];
        private Dictionary<string, StorageResourcesBase> _resourceBuffer = [];
        private Dictionary<string, decimal> _productionItemList = [];
        private bool _cancelFlag;

        public decimal MaxExpectedOtput
        {
            get => _maxExpectedOtput;
            set => _maxExpectedOtput = value;
        }
        public decimal EnergyConsumption
        {
            get => _energyConsumption;
            set => _energyConsumption = value;
        }
        public DateTime LastUpdateTime
        {
            get => _lastUpdateTime;
            set => _lastUpdateTime = value;
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
        //public decimal ProductionTime
        //{
        //    get => _productionTime;
        //    set => _productionTime = value;
        //}
        //public TimeSpan ProgressTime
        //{
        //    get => _progressTime;
        //    set => _progressTime = value;
        //}
        //public TimeSpan ProgressTimeUi
        //{
        //    get => _progressTimeUi;
        //    set => _progressTimeUi = value;
        //}
        //public decimal ProductionTimePerIteration
        //{
        //    get => _productionTimePerIteration;
        //    set => _productionTimePerIteration = value;
        //}
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
        public Dictionary<string, long> RecipeList
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
