namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineLimestone : MineBase
    {
        public MineLimestone((int, int) position) : base(position)
        {
            Name = "Limestone mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Limestone";
        }
    }
}
