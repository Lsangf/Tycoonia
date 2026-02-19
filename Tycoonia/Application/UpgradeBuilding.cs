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
                UpdateUpgradeAmount(building);
            }
        }

        public static void CanUpgrade(dynamic building, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in building.RecipeUpgradeList)
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
            foreach (var item in building.RecipeUpgradeList)
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

        public static void UpdateUpgradeAmount(dynamic building)
        {
            foreach (var item in building.RecipeUpgradeList)
            {
                building.RecipeUpgradeList[item.Key] = item.Value * 2;
            }
            foreach (var item in building.ProductionItemList)
            {
                building.ProductionItemList[item.Key] += (int)Math.Truncate(item.Value * 0.25 + 1);
            }
            building.ProductionRate += (int)Math.Truncate(building.ProductionRate * 0.25 + 1);
            building.EnergyConsumption += 0.01m;
            building.Level += 1;
            building.CanUpgrade = false;
        }
    }
}
