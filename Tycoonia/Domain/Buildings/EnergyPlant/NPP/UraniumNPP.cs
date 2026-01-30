namespace Tycoonia.Domain.Buildings.EnergyPlant.NPP
{
    public class UraniumNPP : EnergyPlantBase
    {
        public UraniumNPP()
        {
            Name = "Uranium Nuclear Power Plant";
            ProductionRate = 10;
            EnergyConsumption = 5;
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ReceipeList = new Dictionary<string, short>
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
