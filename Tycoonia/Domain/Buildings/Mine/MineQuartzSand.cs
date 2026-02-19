namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineQuartzSand : MineBase
    {
        public MineQuartzSand((int, int) position) : base(position)
        {
            Name = "Quartz Sand mine";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Quartz Sand";
        }
    }
}
