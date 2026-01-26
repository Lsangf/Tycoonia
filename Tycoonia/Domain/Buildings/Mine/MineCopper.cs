namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineCopper : MineBase
    {
        public MineCopper((int, int) position) : base(position)
        {
            Name = "Copper mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Copper";
        }
    }
}
