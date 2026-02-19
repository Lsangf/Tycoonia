namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryPlastic : FactoryBase
    {
        public FactoryPlastic()
        {
            Name = "Plastic Factory";
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
                { "Oil", 2 },
                { "Water", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Plastic", ProductionRate }
            };
        }
    }
}
