namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineGravel : MineBase
    {
        public MineGravel((int, int) position) : base(position)
        {
            Name = "Gravel mine";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Gravel";
        }
    }
}
