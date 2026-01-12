namespace Tycoonia.Domain.Buildings.EnergyPlant.TPP
{
    internal class CoalTPP : EnergyPlantBase
    {
        public CoalTPP()
        {
            Name = "Coal Thermal Power Plant";
            Receipe = "Coal";
            ProductionRate = 500;
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100},
                { "Bricks", 100 }
            };
        }
    }
}
