namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryConcrete : FactoryBase
    {
        public FactoryConcrete()
        {
            Type.Add("Concrete");
            Name = "Concrete Factory";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1m;
            EnergyConsumption = 0.5m;
            RecipeList = new Dictionary<string, long>
            {
                { "Money", 10 },
                { "Limestone", 3 },
                { "Gravel", 2 },
                { "Water", 1 }
            };
            ProductionItemList = new Dictionary<string, decimal>
            {
                { "Concrete", ProductionRate }
            };
        }
    }
}
