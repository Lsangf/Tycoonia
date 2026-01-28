using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Storage.Resources
{
    public class StorageResourcesUpgrade
    {
        public static void UpgradeStorage(string resorce, StorageResources storageResources, PlayerReal player)
        {
            StorageResourcesBase upgradeResourceList = storageResources.StorageList[resorce];

            CanUpgradeStorage(player, upgradeResourceList);
            if (!upgradeResourceList.CanUpgrade)
            {
                throw new StorageException();
            }
            else
            {
                UpgradeSubtractionStorage(player, upgradeResourceList);
                UpdateUpgradeAmount(upgradeResourceList);
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
        public static void UpdateUpgradeAmount(StorageResourcesBase upgradeResourceList)
        {
            upgradeResourceList.UpgradeCost *= 2;
            upgradeResourceList.Level += 1;
            upgradeResourceList.CanUpgrade = false;

        }
    }
}
