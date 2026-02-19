namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineGold : MineBase
    {
        public MineGold((int, int) position) : base(position)
        {
            Name = "Gold mine";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Gold";
        }
    }
}
