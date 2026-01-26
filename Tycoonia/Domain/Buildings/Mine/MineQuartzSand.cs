namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineQuartzSand : MineBase
    {
        public MineQuartzSand((int, int) position) : base(position)
        {
            Name = "Quartz Sand mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Quartz Sand";
        }
    }
}
