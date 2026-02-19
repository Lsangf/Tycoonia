namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineLithium : MineBase
    {
        public MineLithium((int, int) position) : base(position)
        {
            Name = "Lithium mine";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Lithium";
        }
    }
}
