namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactorySilicon : FactoryBase
    {
        public FactorySilicon()
        {
            Name = "Silicon Factory";
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
                { "Quartz Sand", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Silicon", ProductionRate }
            };
        }
    }
}
