using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Storage.Resources
{
    public class StorageResourcesUpgrade
    {
        public static void UpgradeStorage(string resource, StorageResources storageResources, PlayerReal player)
        {
            StorageResourcesBase upgradeResourceList = storageResources.StorageList[resource];

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
            player.SubtractSafe(upgradeResourceList.UpgradeCost);
        }
        public static void UpdateUpgradeAmount(StorageResourcesBase upgradeResourceList)
        {            
            upgradeResourceList.UpgradeCost *= 2;
            upgradeResourceList.MaxCapacity += (int)Math.Truncate(upgradeResourceList.MaxCapacity * 0.25 + 1);
            upgradeResourceList.Level += 1;
            upgradeResourceList.CanUpgrade = false;

        }
    }
}
