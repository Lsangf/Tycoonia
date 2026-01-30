namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryCopperWire : FactoryBase
    {
        public FactoryCopperWire()
        {
            Name = "Copper Wire Factory";
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
                { "Copper", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Fuel", ProductionRate }
            };
        }
    }
}
