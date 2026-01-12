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
            ReceipeList = new Dictionary<string, int>
            {
                { "Energy", 1 },
                { "Money", 5 },
                { "Clay", 5 }
            };
        }
    }
}
