namespace Tycoonia.Domain.Buildings.EnergyPlant.TPP
{
    public class SolidFuelTPP : EnergyPlantBase
    {
        public SolidFuelTPP() 
        {
            Name = "Solid Fuel Nuclear Power Plant";
            ProductionRate = 1;
            EnergyConsumption = 5;
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            RecipeList = new Dictionary<string, short>
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
