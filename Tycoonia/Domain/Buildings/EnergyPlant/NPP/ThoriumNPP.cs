namespace Tycoonia.Domain.Buildings.EnergyPlant.NPP
{
    public class ThoriumNPP : EnergyPlantBase
    {
        public ThoriumNPP() 
        {
            Name = "Thorium Nuclear Power Plant";
            ProductionRate = 1;
            EnergyConsumption = 5;
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            RecipeList = new Dictionary<string, short>
            {
                { "Thorium Rod", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                {"Energy", ProductionRate}
            };
        }
    }
}
