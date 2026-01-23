using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Storage.Energy
{
    public class EnergyStorageUpgrade
    {
        public void UpgradeStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
        {
            CanUpgradeStorage(energyStorage, storageResources, player);
            if (!energyStorage.CanUpgrade)
            {
                throw new StorageException();
            }
            else
            {
                UpgradeSubtractionStorage(energyStorage, storageResources, player);
            }
        }

        public bool CanUpgradeStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
        {
            bool canUpgradeStorage = false;
            foreach(var item in energyStorage.ReceipeUpgradeList)
            {
                if (item.Key == "Money" && player.Ballance >= item.Value)
                {
                    canUpgradeStorage = true;
                }
                else if (storageResources.StorageList[item.Key].CurrentQuantity >= item.Value)
                {
                    canUpgradeStorage = true;
                }
                else
                {
                    canUpgradeStorage = false;
                }
            }
            return canUpgradeStorage;
        }

        public void UpgradeSubtractionStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in energyStorage.ReceipeUpgradeList)
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
