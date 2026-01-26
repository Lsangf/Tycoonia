namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineSilver : MineBase
    {
        public MineSilver((int, int) position) : base(position)
        {
            Name = "Silver mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Silver";
        }
    }
}
