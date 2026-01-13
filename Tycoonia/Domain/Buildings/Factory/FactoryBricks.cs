namespace Tycoonia.Domain.Buildings.Factory
{
    internal class FactoryBricks : FactoryBase
    {
        public FactoryBricks()
        {
            Name = "Bricks Factory";
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 50 },
                { "Clay", 30 }
            };
            ProductionRate = 100;
            EnergyConsumption = 0.5f;
            ReceipeList = new Dictionary<string, int>
            {
                { "Money", 5 },
                { "Clay", 5 }
            };
        }
    }
}
