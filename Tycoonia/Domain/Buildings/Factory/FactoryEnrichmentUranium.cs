namespace Tycoonia.Domain.Buildings.Factory
{
    public class FactoryEnrichmentUranium : FactoryBase
    {
        public FactoryEnrichmentUranium()
        {
            Name = "Enrichment Factory";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.5m;
            RecipeList = new Dictionary<string, byte>
            {
                { "Money", 10 },
                { "Uranium", 3 }
            };
            ProductionItemList = new Dictionary<string, int>
            {
                { "Uranium-238", ProductionRate},
                { "Uranium-235", 1}
            };
        }
    }
}
