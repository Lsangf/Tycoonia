namespace Tycoonia.Domain.Buildings.EnergyPlant.TPP
{
    public class FuelTPP : EnergyPlantBase
    {
        public FuelTPP() 
        {
            Name = "Fuel Thermal Power Plant";
            ProductionRate = 10;
            EnergyConsumption = 5;
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ReceipeList = new Dictionary<string, short>
            {
                { "Fuel", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                {"Energy", ProductionRate}
            };
        }
    }
}
