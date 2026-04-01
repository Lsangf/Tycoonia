namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryGlass : FactoryBase
    {
        public FactoryGlass()
        {
            Type.Add("Glass");
            Name = "Glass Factory";
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
                { "Quartz Sand", 3 }
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Glass", ProductionRate }
            };
        }
    }
}
