namespace Tycoonia.Domain.Buildings.EnergyPlant
{
    internal abstract class EnergyPlantBase
    {
        private string _name;
        private short _level;
        private bool _canUpgrade;
        private Dictionary<string, long> _receipeUpgradeList = [];
        private int _productionRate;
        private int _productionTime;
        private int energyConsumption;
        private string _receipe;

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
        public string Receipe
        {
            get => _receipe;
            set => _receipe = value;
        }

        protected EnergyPlantBase()
        {
            Level = 1;
            CanUpgrade = false;
        }
    }
}
