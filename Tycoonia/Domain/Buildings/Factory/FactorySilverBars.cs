namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactorySilverBars : FactoryBase
    {
        public FactorySilverBars()
        {
            Name = "Silver Bars Factory";
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
                { "Silver", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Silver Bars", ProductionRate }
            };
        }
    }
}
