using Tycoonia.Domain.Buildings.EnergyPlant;

namespace Tycoonia.Presentation.UI.Info
{
    public class EnergyPlantInfo
    {
        public static void ShowEnergyPlantInfo(EnergyPlantBase energyPlant)
        {
            string RecipeUpgradeListString = "";
            foreach (var item in energyPlant.RecipeUpgradeList)
            {
                RecipeUpgradeListString += $"| {item.Key}: {item.Value} | ";
            }
            string ProductionItemListString = "";
            foreach (var item in energyPlant.ProductionItemList)
            {
                ProductionItemListString += $"| {item.Key}: {item.Value} | ";
            }
            string RecipeListString = "";
            foreach (var item in energyPlant.RecipeList)
            {
                RecipeListString += $"| {item.Key}: {item.Value} | ";
            }
            string ResourceBufferString = "";
            foreach (var item in energyPlant.ResourceBuffer)
            {
                ResourceBufferString += $"| {item.Key}: {item.Value.CurrentQuantity} | ";
            }

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Name:                 {energyPlant.Name}");
            Console.WriteLine($"LVL:                  {energyPlant.Level}");
            Console.WriteLine($"Can Upgrade:          {energyPlant.CanUpgrade}");
            Console.WriteLine($"Recipe Upgrade List   {RecipeUpgradeListString}");
            Console.WriteLine($"Production Rate:      {energyPlant.ProductionRate}");
            Console.WriteLine($"Energy Consumption:   {energyPlant.EnergyConsumption}");
            Console.WriteLine($"Production Time:      {energyPlant.ProductionTime}");
            Console.WriteLine($"Recipe List:          {RecipeListString}");
            Console.WriteLine($"Resource Buffer:      {ResourceBufferString}");
            Console.WriteLine($"Production Item List: {ProductionItemListString}");
            Console.WriteLine($"Work Flag:            {energyPlant.WorkFlag}");
        }
    }
}
