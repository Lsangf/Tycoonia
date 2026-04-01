namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryPurifiedLithium : FactoryBase
    {
        public FactoryPurifiedLithium()
        {
            Type.Add("Purified Lithium");
            Name = "Purified Lithium Factory";
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
                { "Lithium", 3 }
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Purified Lithium", ProductionRate }
            };
        }
    }
}

