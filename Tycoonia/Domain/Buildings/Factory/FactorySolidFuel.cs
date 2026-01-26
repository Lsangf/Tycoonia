namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactorySolidFuel : FactoryBase
    {
        public FactorySolidFuel()
        {
            Name = "Solid Fuel Factory";
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
                { "Fuel", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Solid Fuel", ProductionRate }
            };
        }
    }
}
