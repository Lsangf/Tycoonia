using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Presentation.UI.Info
{
    public class CurrentStorageResourceInfo
    {
        public static void ShowCurrentStorageResourceInfo(string currentResourceName, StorageResourcesBase currentStorageResourcesValue)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Name:             {currentResourceName}");
            Console.WriteLine($"LVL:              {currentStorageResourcesValue.Level}");
            Console.WriteLine($"Current Quantity: {currentStorageResourcesValue.CurrentQuantity}");
            Console.WriteLine($"Max Capacity:     {currentStorageResourcesValue.MaxCapacity}");
            Console.WriteLine($"Can Upgrade:      {currentStorageResourcesValue.CanUpgrade}");
            Console.WriteLine($"Upgrade Cost      {currentStorageResourcesValue.UpgradeCost} $");
        }
    }
}
