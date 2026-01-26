namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineIron : MineBase
    {
        public MineIron((int, int) position) : base(position)
        {
            Name = "Iron mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Iron";
        }
    }
}
