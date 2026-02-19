using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Mining
{
    public class MineUpgrade
    {
        public static void Upgrade(MineBase mine, StorageResources storageResources, PlayerReal player)
        {
            CanUpgrade(mine, storageResources, player);
            if (!mine.CanUpgrade)
            {
                throw new StorageException();
            }
            else if (mine.CanUpgrade && mine.Level < mine.MAXLevel)
            {
                UpgradeSubtraction(mine, storageResources, player);
                UpdateUpgradeAmount(mine);
            }
            else
            {
                throw new LvlException();
            }
        }

        public static void CanUpgrade(MineBase mine, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in mine.RecipeUpgradeList)
            {
                if (item.Key == "Money" && player.Ballance >= item.Value)
                {
                    mine.CanUpgrade = true;
                }
                else if (storageResources.StorageList[item.Key].CurrentQuantity >= item.Value)
                {
                    mine.CanUpgrade = true;
                }
                else
                {
                    mine.CanUpgrade = false;
                }
            }
        }

        public static void UpgradeSubtraction(MineBase mine, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in mine.RecipeUpgradeList)
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

        public static void UpdateUpgradeAmount(MineBase mine)
        {
            foreach (var item in mine.RecipeUpgradeList)
            {
                mine.RecipeUpgradeList[item.Key] = item.Value * 2;
            }
            mine.ProductionRate += (int)Math.Truncate(mine.ProductionRate * 0.25 + 1);
            mine.EnergyConsumption += 0.01m;
            mine.Level += 1;
            mine.CanUpgrade = false;
        }
    }
}
