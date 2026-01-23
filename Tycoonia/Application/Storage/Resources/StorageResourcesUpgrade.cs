using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Storage.Resources
{
    public class StorageResourcesUpgrade
    {
        public void UpgradeStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
        {
            string upgradeResourceString = storageResources.StorageList.Keys.First();
            StorageResourcesBase upgradeResourceList = storageResources.StorageList[upgradeResourceString];

            CanUpgradeStorage(energyStorage, storageResources, player, upgradeResourceList);
            if (!storageResources.CanUpgrade)
            {
                throw new StorageException();
            }
            else
            {
                UpgradeSubtractionStorage(energyStorage, storageResources, player, upgradeResourceList);
            }
        }

        public bool CanUpgradeStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player, StorageResourcesBase upgradeResourceList)
        {
            bool canUpgradeStorage = false;
            if (player.Ballance >= upgradeResourceList.UpgradeCost)
            {
                canUpgradeStorage = true;
            }
            else
            {
                canUpgradeStorage = false;
            }
            return canUpgradeStorage;
        }

        public void UpgradeSubtractionStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player, StorageResourcesBase upgradeResourceList)
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
