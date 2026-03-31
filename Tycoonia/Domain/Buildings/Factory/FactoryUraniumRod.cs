namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryUraniumRod : FactoryBase
    {
        public FactoryUraniumRod()
        {
            Type.Add("Uranium Rod");
            Name = "Uranium Rod Factory";
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
                { "Uranium-238", 1},
                { "Uranium-235", 10}
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Uranium Rod", ProductionRate }
            };
        }
    }
}
