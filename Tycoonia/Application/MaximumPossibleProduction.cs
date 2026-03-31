using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application
{
    public class MaximumPossibleExpectedOtput
    {
        public static void UpdateMaximumPossibleExpectedOtput(dynamic building, StorageResources storageResources, PlayerReal player)
        {
            List<decimal> resultProduction = [];
            foreach(var item in building.RecipeList)
            {
                if (item.Key == "Money")
                {
                    resultProduction.Add(player.Ballance / item.Value);
                }
                else
                {
                    resultProduction.Add(storageResources.StorageList[item.Key].CurrentQuantity / item.Value);
                }
            }
            building.MaxExpectedOtput = resultProduction.Min();
        }
    }
}
