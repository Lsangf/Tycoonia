using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Storage.Energy
{
    public class EnergyStorageUpgrade
    {
        public static void UpgradeStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
        {
            CanUpgradeStorage(energyStorage, storageResources, player);
            if (!energyStorage.CanUpgrade)
            {
                throw new StorageException();
            }
            else
            {
                UpgradeSubtractionStorage(energyStorage, storageResources, player);
                UpdateUpgradeAmount(energyStorage);
            }
        }

        public static void CanUpgradeStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
        {
            foreach(var item in energyStorage.ReceipeUpgradeList)
            {
                if (item.Key == "Money" && player.Ballance >= item.Value)
                {
                    energyStorage.CanUpgrade = true;
                }
                else if (storageResources.StorageList[item.Key].CurrentQuantity >= item.Value)
                {
                    energyStorage.CanUpgrade = true;
                }
                else
                {
                    energyStorage.CanUpgrade = false;
                }
            }
        }

        public static void UpgradeSubtractionStorage(EnergyStorage energyStorage, StorageResources storageResources, PlayerReal player)
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

        public static void UpdateUpgradeAmount(EnergyStorage energyStorage)
        {
            foreach (var item in energyStorage.ReceipeUpgradeList)
            {
                energyStorage.ReceipeUpgradeList[item.Key] = item.Value * 2;
            }
            energyStorage.Level += 1;
            energyStorage.CanUpgrade = false;

        }
    }
}
