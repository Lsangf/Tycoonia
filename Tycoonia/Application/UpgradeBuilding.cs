using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application
{
    public class UpgradeBuilding
    {
        public static void Upgrade(dynamic building, StorageResources storageResources, PlayerReal player)
        {
            CanUpgrade(building, storageResources, player);
            if (!building.CanUpgrade)
            {
                throw new StorageException();
            }
            else
            {
                UpgradeSubtraction(building, storageResources, player);
            }
        }

        public static void CanUpgrade(dynamic building, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in building.ReceipeUpgradeList)
            {
                if (item.Key == "Money" && player.Ballance >= item.Value)
                {
                    building.CanUpgrade = true;
                }
                else if (storageResources.StorageList[item.Key].CurrentQuantity >= item.Value)
                {
                    building.CanUpgrade = true;
                }
                else
                {
                    building.CanUpgrade = false;
                }
            }
        }

        public static void UpgradeSubtraction(dynamic building, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in building.ReceipeUpgradeList)
            {
                if (item.Key == "Money")
                {
                    player.Ballance -= item.Value;
                }
                else if (storageResources.StorageList[item.Key].CurrentQuantity >= item.Value)
                {
                    storageResources.StorageList[item.Key].CurrentQuantity -= item.Value;
                }
                else
                {
                    throw new StorageException();
                }
            }
        }
    }
}
