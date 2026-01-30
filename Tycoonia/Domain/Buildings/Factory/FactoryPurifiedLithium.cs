namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryPurifiedLithium : FactoryBase
    {
        public FactoryPurifiedLithium()
        {
            Name = "Purified Lithium Factory";
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
                { "Lithium", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Purified Lithium", ProductionRate }
            };
        }
    }
}
