namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactorySteel : FactoryBase
    {
        public FactorySteel()
        {
            Name = "Steel Factory";
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
                { "Iron", 3 },
                { "Coal", 2 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Steel", ProductionRate }
            };
        }
    }
}
