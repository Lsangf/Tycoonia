namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineOil : MineBase
    {
        public MineOil((int, int) position) : base(position)
        {
            Name = "Oil well";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Oil";
        }
    }
}
