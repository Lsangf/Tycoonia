namespace Tycoonia.Domain.Buildings.EnergyPlant.TPP
{
    internal class CoalTPP : EnergyPlantBase
    {
        public CoalTPP()
        {
            Name = "Coal Thermal Power Plant";
            ProductionRate = 500;
            EnergyConsumption = 5;
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100},
                { "Bricks", 100 }
            };
            Receipe = "Coal";
        }
    }
}
