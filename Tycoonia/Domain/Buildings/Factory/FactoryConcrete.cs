namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryConcrete : FactoryBase
    {
        public FactoryConcrete()
        {
            Name = "Concrete Factory";
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
                { "Limestone", 3 },
                { "Gravel", 2 },
                { "Water", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Concrete", ProductionRate }
            };
        }
    }
}
