namespace Tycoonia.Domain.Buildings.EnergyPlant.TPP
{
    public class CoalTPP : EnergyPlantBase
    {
        public CoalTPP()
        {
            Name = "Coal Thermal Power Plant";
            ProductionRate = 5;
            EnergyConsumption = 5;
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100},
                { "Bricks", 100 }
            };
            ReceipeList = new Dictionary<string, short>
            {
                { "Coal", 10 }
            };
        }
    }
}
