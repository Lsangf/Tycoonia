namespace Tycoonia.Domain.Buildings.Mine
{
    public class MineThorium232 : MineBase
    {
        public MineThorium232((int, int) position) : base(position)
        {
            Name = "Thorium-232 mine";
            ProductionRate = 1;
            EnergyConsumption = 0.1m;
            ProductionItem = "Thorium-232";
        }
    }
}
