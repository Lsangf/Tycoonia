namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineClay : MineBase
    {
        public MineClay((int, int) position) : base(position)
        {
            Name = "Clay";
            ProductionRate = 5;
            EnergyConsumption = 0.1f;
        }
    }
}
