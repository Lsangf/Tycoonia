namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineBauxite : MineBase
    {
        public MineBauxite((int, int) position) : base(position)
        {
            Name = "Bauxite mine";
            RecipeUpgradeList = new Dictionary<string, long>
            {
                { "Money", 100 },
                { "Bricks", 1 }
            };
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Bauxite";
        }
    }
}
