namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryDiamonds : FactoryBase
    {
        public FactoryDiamonds()
        {
            Type.Add("Diamonds");
            Name = "Diamond Refinery";
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
                { "Rough Diamonds", 3 }
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Diamonds", ProductionRate }
            };
        }
    }
}
