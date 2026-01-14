namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineCoal : MineBase
    {
        public MineCoal((int, int) position) : base(position)
        {
            Name = "Coal";
            ProductionRate = 1;
            EnergyConsumption = 0.1f;
        }
    }
}
