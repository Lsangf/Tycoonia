using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Presentation.UI.Info
{
    public class StorageResourcesInfo
    {
        public static void ShowStorageResourcesInfo(StorageResources storageResources)
        {
            foreach (var resource in storageResources.StorageList)
            {
                Console.WriteLine("");
                string resourceName = resource.Key;
                string quantityText = $"{resource.Value.CurrentQuantity} / {resource.Value.MaxCapacity}";

                int consoleWidth = Console.WindowWidth;
                int quantityLength = quantityText.Length;
                int centerStartPos = (consoleWidth / 5);
                int paddingNeeded = centerStartPos - resourceName.Length;
                if (paddingNeeded < 1) paddingNeeded = 1;

                string formattedLine = resourceName + new string(' ', paddingNeeded) + quantityText;
                if (formattedLine.Length > consoleWidth)
                {
                    formattedLine = formattedLine.Substring(0, consoleWidth);
                }
                Console.WriteLine(formattedLine);
            }
        }
    }
}
