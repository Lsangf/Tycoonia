namespace Tycoonia.Domain.Buildings.Mine
{
    internal class MineCoal : MineBase
    {
        public MineCoal((int, int) position) : base(position)
        {
            Name = "Coal mine";
            ProductionRate = 20;
        }
    }
}
