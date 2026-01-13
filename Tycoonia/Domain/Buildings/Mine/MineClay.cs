namespace Tycoonia.Domain.Buildings.Mine
{
    internal class MineClay : MineBase
    {
        public MineClay((int, int) position) : base(position)
        {
            Name = "Clay Mine";
            ProductionRate = 15;
            EnergyConsumption = 0.1f;
        }
    }
}
