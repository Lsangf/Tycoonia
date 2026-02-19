using Tycoonia.Application.Storage.Energy;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI.Info;

namespace Tycoonia.Presentation.UI
{
    public class EnergyStorageSystem
    {
        public static void ActionsEnergyStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
        {
            EnergyStorageInfo.ShowEnergyStorageInfo(energyStorage);
            ChoiceActionsEnergyStorage(energyStorage, storageResources, player);
        }

        public static void ChoiceActionsEnergyStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
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
                        EnergyStorageUpgrade.UpgradeStorage(energyStorage, storageResources, player);
                        Console.WriteLine($"Upgrade completed | LVL {energyStorage.Level} |\n");
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
