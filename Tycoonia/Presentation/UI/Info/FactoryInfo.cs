using Tycoonia.Domain.Buildings.Factory;

namespace Tycoonia.Presentation.UI.Info
{
    public class FactoryInfo
    {
        public static void ShowFactoryInfo(FactoryBase factory)
        {
            string RecipeUpgradeListString = "";
            foreach (var item in factory.RecipeUpgradeList)
            {
                RecipeUpgradeListString += $"| {item.Key}: {item.Value} | ";
            }
            string ProductionItemListString = "";
            foreach (var item in factory.ProductionItemList)
            {
                ProductionItemListString += $"| {item.Key}: {item.Value} | ";
            }
            string RecipeListString = "";
            foreach (var item in factory.RecipeList)
            {
                RecipeListString += $"| {item.Key}: {item.Value} | ";
            }
            string ResourceBufferString = "";
            foreach (var item in factory.ResourceBuffer)
            {
                ResourceBufferString += $"| {item.Key}: {item.Value.CurrentQuantity} | ";
            }

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Name:                 {factory.Name}");
            Console.WriteLine($"LVL:                  {factory.Level}");
            Console.WriteLine($"Can Upgrade:          {factory.CanUpgrade}");
            Console.WriteLine($"Recipe Upgrade List   {RecipeUpgradeListString}");
            Console.WriteLine($"Production Rate:      {factory.ProductionRate}");
            Console.WriteLine($"Energy Consumption:   {factory.EnergyConsumption}");
            Console.WriteLine($"Production Time:      {factory.ProductionTime}");
            Console.WriteLine($"Recipe List:          {RecipeListString}");
            Console.WriteLine($"Resource Buffer:      {ResourceBufferString}");
            Console.WriteLine($"Production Item List: {ProductionItemListString}");
            Console.WriteLine($"Work Flag:            {factory.WorkFlag}");
            Console.WriteLine($"Cancel Flag:          {factory.CancelFlag}");
        }
    }
}
