namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryGoldBars : FactoryBase
    {
        public FactoryGoldBars()
        {
            Name = "Gold Bars Factory";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.5m;
            RecipeList = new Dictionary<string, byte>
            {
                { "Money", 10 },
                { "Gold", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Gold Bars", ProductionRate }
            };
        }
    }
}
