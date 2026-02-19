namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineClay : MineBase
    {
        public MineClay((int, int) position) : base(position)
        {
            Name = "Clay mine";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 5;
            EnergyConsumption = 0.1m;
            ProductionItem = "Clay";
        }
    }
}
