using Tycoonia.Application.Storage;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application
{
    public class ReservationBool
    {
        public static bool ResourcesReservation(Dictionary<string, StorageResourcesBase> resourcesBuffer, StorageResources storageResources, PlayerReal player)
        {
            bool checkValue = false;
            foreach (var item in resourcesBuffer)
            {
                if (item.Key == "Money" && player.Ballance >= item.Value.CurrentQuantity)
                {
                    checkValue = true;
                }
                else if (storageResources.StorageList[item.Key].CurrentQuantity >= item.Value.CurrentQuantity)
                {
                    checkValue = true;
                }
                else
                {
                    checkValue = false;
                }
            }
            return checkValue;
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
