namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryBatteries : FactoryBase
    {
        public FactoryBatteries()
        {
            Name = "Batteries Factory";
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.5m;
            RecipeList = new Dictionary<string, byte>
            {
                { "Money", 10 },
                { "Purified Lithium", 2 },
                { "Copper Wire", 1 },
                { "Titanium", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Batteries", ProductionRate }
            };
        }
    }
}
