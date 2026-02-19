using Tycoonia.Application.Mining;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI.Info;

namespace Tycoonia.Presentation.UI
{
    public class MineSystem
    {
        public static void ActionsMine(List<MineBase> mines, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            for (int index = 1; (index - 1) < mines.Count; index++)
            {
                Console.WriteLine($"[{index}] {mines[index - 1].Name}");
            }
            Console.WriteLine("\nChoice number factory...");
            byte choiceNumberMine = (byte)ConsoleInput.ConsoleChoice();
            MineBase currentMine = mines[choiceNumberMine - 1];

            MineInfo.ShowMineInfo(currentMine);
            ChoiceMineActions(currentMine, storageResources, energyStorage, player);
        }

        public static void ChoiceMineActions(MineBase currentMine, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            Console.WriteLine("\nChoose an actions:");
            Console.WriteLine("1. Start of production");
            Console.WriteLine("2. Upgrade");
            Console.WriteLine("3. Exit");
            byte choiceActions = (byte)ConsoleInput.ConsoleChoice();

            switch (choiceActions)
            {
                case 1:
                    UpdateMineCalculations(storageResources, currentMine, energyStorage, player);
                    break;
                case 2:
                    try
                    {
                        MineUpgrade.Upgrade(currentMine, storageResources, player);
                        Console.WriteLine($"Upgrade completed | LVL {currentMine.Level} |\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    break;
                case 3:
                    Console.WriteLine("Exiting mine actions.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public static void UpdateMineCalculations(StorageResources storageResources, MineBase currentMine, EnergyStorage energyStorage, PlayerReal player)
        {
            MineInfo.ShowMineInfo(currentMine);
            while (currentMine.WorkFlag)
            {
                ResourceExtraction.ResourceExtractionMine(storageResources, currentMine, energyStorage);
                MineInfo.ShowMineInfo(currentMine);
                StorageResourcesInfo.ShowStorageResourcesInfo(storageResources);
                currentMine.WorkFlag = false;
            }
            currentMine.WorkFlag = false;
        }
    }
}
