using Tycoonia.Application.Storage.Resources;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI.Info;

namespace Tycoonia.Presentation.UI
{
    public class StorageResourcesSystem
    {
        public static void ActionsStorageResources(StorageResources storageResources, PlayerReal player)
        {
            List<string> StorageResourcesListName = [];
            List<StorageResourcesBase> storageResourcesList = [];
            StorageResourcesInfo.ShowStorageResourcesInfo(storageResources);
            int index = 1;
            foreach (var item in storageResources.StorageList)
            {
                Console.WriteLine($"[{index}] {item.Key}");
                StorageResourcesListName.Add(item.Key);
                storageResourcesList.Add(item.Value);
                index++;
            }
            Console.WriteLine("\nChoice number resource...");
            byte choiceNumberResource = (byte)ConsoleInput.ConsoleChoice();
            string currentResourceName = StorageResourcesListName[choiceNumberResource - 1];
            StorageResourcesBase currentResource = storageResourcesList[choiceNumberResource - 1];

            CurrentStorageResourceInfo.ShowCurrentStorageResourceInfo(currentResourceName, storageResources.StorageList[currentResourceName]);
            ChoiceActionsStorageResources(storageResources.StorageList[currentResourceName], currentResourceName, storageResources, player);
        }

        public static void ChoiceActionsStorageResources(StorageResourcesBase currentResource, string currentResourceName,  StorageResources storageResources, PlayerReal player)
        {
            Console.WriteLine("\nChoose an actions:");
            Console.WriteLine("1. Upgrade");
            Console.WriteLine("2. Exit");
            byte choiceActions = (byte)ConsoleInput.ConsoleChoice();

            switch (choiceActions)
            {
                case 1:
                    try
                    {
                        StorageResourcesUpgrade.UpgradeStorage(currentResourceName, storageResources, player);
                        Console.WriteLine($"Upgrade completed | LVL {currentResource.Level} |\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    break;
                case 2:
                    Console.WriteLine("Exiting storage resources actions.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
