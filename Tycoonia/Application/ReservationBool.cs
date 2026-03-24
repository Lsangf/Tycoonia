using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application
{
    public class ReservationBool
    {
        public static bool ResourcesReservation(Dictionary<string, StorageResourcesBase> resourcesBuffer, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in resourcesBuffer)
            {
                if (item.Key == "Money")
                {
                    if (player.Ballance < item.Value.CurrentQuantity)
                        return false;
                }
                else
                {
                    if (storageResources.StorageList[item.Key].CurrentQuantity < item.Value.CurrentQuantity)
                        return false;
                }
            }

            return true;
        }

        public static bool EnergyReservation(decimal energyConsumption, EnergyStorage energyStorage)
        {
            if (energyStorage.CurrentStorage >= energyConsumption)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
