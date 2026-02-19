namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryBricks : FactoryBase
    {
        public FactoryBricks()
        {
            Name = "Bricks Factory";
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
                { "Clay", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Bricks", ProductionRate}
            };
        }
    }
}
