namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryElectronicComponents : FactoryBase
    {
        public FactoryElectronicComponents()
        {
            Name = "Electronic Components Factory";
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
                { "Silicon", 2 },
                { "Copper Wire", 1 },
                { "Plastic", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Electronic Components", ProductionRate }
            };
        }
    }
}
