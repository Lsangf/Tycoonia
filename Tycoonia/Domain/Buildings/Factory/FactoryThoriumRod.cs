using System.Xml.Linq;

namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryThoriumRod : FactoryBase
    {
        public FactoryThoriumRod()
        {
            Name = "Thorium Rod Factory";
            ReceipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 1m;
            RecipeList = new Dictionary<string, byte>
            {
                { "Money", 10 },
                { "Thorium-232", 1},
                { "Plutonium-239", 1 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Thorium Rod", ProductionRate }
            };
        }
    }
}
