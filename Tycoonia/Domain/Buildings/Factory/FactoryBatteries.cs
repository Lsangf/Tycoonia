namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryBatteries : FactoryBase
    {
        public FactoryBatteries()
        {
            Type.Add("Batteries");
            Name = "Batteries Factory";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1m;
            EnergyConsumption = 0.5m;
            RecipeList = new Dictionary<string, byte>
            {
                { "Money", 10 },
                { "Purified Lithium", 2 },
                { "Copper Wire", 1 },
                { "Titanium", 1 }
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Batteries", ProductionRate }
            };
        }
    }
}
