namespace Tycoonia.Domain.Buildings.EnergyPlant.TPP
{
    public class SolidFuelTPP : EnergyPlantBase
    {
        public SolidFuelTPP() 
        {
            Name = "Solid Fuel Nuclear Power Plant";
            ProductionRate = 50;
            EnergyConsumption = 20;
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ReceipeList = new Dictionary<string, short>
            {
                { "Solid Fuel", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                {"Energy", ProductionRate}
            };
        }
    }
}
