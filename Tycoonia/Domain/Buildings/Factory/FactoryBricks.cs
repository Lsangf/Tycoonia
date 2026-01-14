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
                { "Clay", 30 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.5f;
            ReceipeList = new Dictionary<string, byte>
            {
                { "Money", 10 },
                { "Clay", 3 }
            };
        }
    }
}
