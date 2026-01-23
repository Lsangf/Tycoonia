using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Storage.Resources
{
    public class StorageResourcesUpgrade
    {
        public static void UpgradeStorage(StorageResources storageResources, PlayerReal player)
        {
            string upgradeResourceString = storageResources.StorageList.Keys.First();
            StorageResourcesBase upgradeResourceList = storageResources.StorageList[upgradeResourceString];

            CanUpgradeStorage(player, upgradeResourceList);
            if (!upgradeResourceList.CanUpgrade)
            {
                throw new StorageException();
            }
            else
            {
                UpgradeSubtractionStorage(player, upgradeResourceList);
            }
        }

        public static void CanUpgradeStorage(PlayerReal player, StorageResourcesBase upgradeResourceList)
        {
            if (player.Ballance >= upgradeResourceList.UpgradeCost)
            {
                upgradeResourceList.CanUpgrade = true;
            }
            else
            {
                upgradeResourceList.CanUpgrade = false;
            }
        }

        public static void UpgradeSubtractionStorage(PlayerReal player, StorageResourcesBase upgradeResourceList)
        {
            if (player.Ballance >= upgradeResourceList.UpgradeCost)
            {
                player.Ballance -= upgradeResourceList.UpgradeCost;
            }
            else
            {
                throw new StorageException();
            }
        }
    }
}
