namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineGold : MineBase
    {
        public MineGold((int, int) position) : base(position)
        {
            Name = "Gold mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Gold";
        }
    }
}
