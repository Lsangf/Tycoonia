namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryEnergyStorage : FactoryBase
    {
        public FactoryEnergyStorage()
        {
            Name = "Storage Factory";
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
                { "Batteries", 3 },
                { "Steel", 2 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Energy Storage", ProductionRate }
            };
        }
    }
}
