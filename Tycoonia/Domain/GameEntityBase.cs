namespace Tycoonia.Domain
{
    public abstract class GameEntityBase : EntityBase
    {
        private string _name;
        private short _level;
        private bool _canUpgrade;
        private Dictionary<string, long> _recipeUpgradeList = new();
        private int _productionRate;
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
        public bool WorkFlag
        {
            get => _workFlag;
            set => _workFlag = value;
        }
    }
}
