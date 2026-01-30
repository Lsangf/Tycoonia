namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryTitanium : FactoryBase
    {
        public FactoryTitanium()
        {
            Name = "Titanium Factory";
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
                { "Limenite", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Titanium", ProductionRate }
            };
        }
    }
}
