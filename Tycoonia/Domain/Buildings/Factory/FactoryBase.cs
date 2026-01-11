namespace Tycoonia.Domain.Buildings.Factory
{
    internal class FactoryBase
    {
        private string _name;
        private short _level;
        private int _productionRate;
        private int _productionTime;
        private Dictionary<string, int> _receipeList = [];
        private Dictionary<string, int> _receipeUpgradeList = [];
        private int _CancelFlag;




    }
}
