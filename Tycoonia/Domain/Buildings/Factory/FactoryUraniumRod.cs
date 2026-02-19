namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryUraniumRod : FactoryBase
    {
        public FactoryUraniumRod()
        {
            Name = "Uranium Rod Factory";
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
                { "Uranium-238", 1},
                { "Uranium-235", 10}
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Uranium Rod", ProductionRate }
            };
        }
    }
}
