namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineWater : MineBase
    {
        public MineWater((int, int) position) : base(position)
        {
            Name = "Water well";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Water";
        }
    }
}
