namespace Tycoonia.Domain.Buildings.EnergyPlant.TPP
{
    public class CoalTPP : EnergyPlantBase
    {
        public CoalTPP()
        {
            Name = "Coal Thermal Power Plant";
            ProductionRate = 1;
            EnergyConsumption = 5;
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            RecipeList = new Dictionary<string, short>
            {
                { "Coal", 10 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                {"Energy", ProductionRate}
            };
        }
    }
}
