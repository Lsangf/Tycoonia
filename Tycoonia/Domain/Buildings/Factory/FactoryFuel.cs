namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryFuel : FactoryBase
    {
        public FactoryFuel()
        {
            Type.Add("Fuel");
            Name = "Fuel Factory";
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
                { "Oil", 3 }
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Fuel", ProductionRate }
            };
        }
    }
}

