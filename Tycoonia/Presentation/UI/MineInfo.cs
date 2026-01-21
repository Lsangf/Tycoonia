using Tycoonia.Domain.Buildings.Mine;

namespace Tycoonia.Presentation.UI
{
    public class MineInfo
    {
        public static void ShowMineInfo(MineBase mine)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Name:               {mine.Name}");
            Console.WriteLine($"LVL:                {mine.Level}");
            Console.WriteLine($"Can Upgrade:        {mine.CanUpgrade}");
            Console.WriteLine($"Production Rate:    {mine.ProductionRate}");
            Console.WriteLine($"Energy Consumption: {mine.EnergyConsumption}");
            Console.WriteLine($"State:              {mine.State}");
            Console.WriteLine($"Position:           {mine.Position}");
            Console.WriteLine($"Production Item:    {mine.ProductionItem}");
            Console.WriteLine($"Work Flag:          {mine.WorkFlag}");
            Console.WriteLine($"Cancel Flag:        {mine.CancelFlag}");
        }
    }
}
