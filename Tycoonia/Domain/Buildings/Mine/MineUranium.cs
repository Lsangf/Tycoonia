namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineUranium : MineBase
    {
        public MineUranium((int x, int y) position) : base(position)
        {
            Name = "Uranium Mine";
            ProductionRate = 5;
            EnergyConsumption = 0.1m;
            ProductionItem = "Uranium";
        }
    }
}
