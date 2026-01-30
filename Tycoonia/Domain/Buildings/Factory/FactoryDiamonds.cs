namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryDiamonds : FactoryBase
    {
        public FactoryDiamonds()
        {
            Name = "Diamond Refinery";
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
                { "Rough Diamonds", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Diamonds", ProductionRate }
            };
        }
    }
}
