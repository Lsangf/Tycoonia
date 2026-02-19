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
            foreach(var item in energyStorage.RecipeUpgradeList)
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
            foreach (var item in energyStorage.RecipeUpgradeList)
            {
                if (item.Key == "Money")
                {
                    player.SubtractSafe(item.Value);
                }
                else
                {
                    storageResources.SubtractResourceSafe(item.Key, item.Value);
                }
            }
        }

        public static void UpdateUpgradeAmount(EnergyStorage energyStorage)
        {
            foreach (var item in energyStorage.RecipeUpgradeList)
            {
                energyStorage.RecipeUpgradeList[item.Key] = item.Value * 2;
            }
            energyStorage.MaxCapacity += Math.Round(energyStorage.MaxCapacity * 0.25m + 1m, 2);
            energyStorage.Level += 1;
            energyStorage.CanUpgrade = false;

        }
    }
}
