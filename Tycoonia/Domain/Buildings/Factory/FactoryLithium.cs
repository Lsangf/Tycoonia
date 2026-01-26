namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryLithium : FactoryBase
    {
        public FactoryLithium()
        {
            Name = "Purified Lithium Factory";
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
                { "Lithium", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Purified Lithium", ProductionRate }
            };
        }
    }
}
