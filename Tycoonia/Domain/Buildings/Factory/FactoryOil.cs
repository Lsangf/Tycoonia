namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryOil : FactoryBase
    {
        public FactoryOil()
        {
            Name = "Fuel Factory";
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 1m;
            RecipeList = new Dictionary<string, byte>
            {
                { "Money", 10 },
                { "Oil", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Fuel", ProductionRate }
            };
        }
    }
}
