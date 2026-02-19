using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Presentation.UI.Info
{
    public class EnergyStorageInfo
    {
        public static void ShowEnergyStorageInfo(EnergyStorage energyStorage)
        {
            string RecipeUpgradeListString = "";
            foreach (var item in energyStorage.RecipeUpgradeList)
            {
                RecipeUpgradeListString += $"| {item.Key}: {item.Value} | ";
            }
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Name:            Energy");
            Console.WriteLine($"LVL:             {energyStorage.Level}");
            Console.WriteLine($"Current Storage: {energyStorage.CurrentStorage}");
            Console.WriteLine($"Max Capacity:    {energyStorage.MaxCapacity}");
            Console.WriteLine($"Can Upgrade:     {energyStorage.CanUpgrade}");
            Console.WriteLine($"Upgrade List:    {RecipeUpgradeListString}");
        }
    }
}
