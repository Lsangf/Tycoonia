namespace Tycoonia.Domain.Buildings.EnergyPlant.NPP
{
    public class UraniumNPP : EnergyPlantBase
    {
        public UraniumNPP()
        {
            Name = "Uranium Nuclear Power Plant";
            ProductionRate = 1;
            EnergyConsumption = 5;
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            RecipeList = new Dictionary<string, short>
            {
                { "Uranium Rod", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                {"Energy", ProductionRate},
                { "Plutonium-239", 1}
            };
        }
    }
}
