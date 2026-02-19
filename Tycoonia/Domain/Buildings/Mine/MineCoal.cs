namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineCoal : MineBase
    {
        public MineCoal((int, int) position) : base(position)
        {
            Name = "Coal mine";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Coal";
        }
    }
}
