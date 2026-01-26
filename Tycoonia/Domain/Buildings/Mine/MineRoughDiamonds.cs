namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineRoughDiamonds : MineBase
    {
        public MineRoughDiamonds((int, int) position) : base(position)
        {
            Name = "Rough Diamonds mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Rough Diamonds";
        }
    }
}
