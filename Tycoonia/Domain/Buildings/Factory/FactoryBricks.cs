namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryBricks : FactoryBase
    {
        public FactoryBricks()
        {
            Name = "Bricks Factory";
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 50 },
                { "Clay", 3 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.5m;
            ReceipeList = new Dictionary<string, byte>
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
