namespace Tycoonia.Domain.Buildings.EnergyPlant.NPP
{
    public class UraniumNPP : EnergyPlantBase
    {
        public UraniumNPP()
        {
            Name = "Uranium Nuclear Power Plant";
            ProductionRate = 50;
            EnergyConsumption = 20;
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 1000},
                { "Bricks", 1000 }
            };
            ReceipeList = new Dictionary<string, short>
            {
                { "Uranium-235", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                {"Energy", ProductionRate},
                { "Plutonium-239", 1}
            };
        }
    }
}
