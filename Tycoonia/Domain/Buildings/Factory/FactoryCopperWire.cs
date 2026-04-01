namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryCopperWire : FactoryBase
    {
        public FactoryCopperWire()
        {
            Type.Add("Copper Wire");
            Name = "Copper Wire Factory";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1m;
            EnergyConsumption = 0.5m;
            RecipeList = new Dictionary<string, long>
            {
                { "Money", 10 },
                { "Copper", 3 }
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Copper Wire", ProductionRate }
            };
        }
    }
}
