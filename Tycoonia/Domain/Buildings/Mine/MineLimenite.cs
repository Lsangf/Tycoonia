namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineLimenite : MineBase
    {
        public MineLimenite((int, int) position) : base(position)
        {
            Name = "Limenite mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Limenite";
        }
    }
}
