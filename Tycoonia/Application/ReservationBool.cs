using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application
{
    public class ReservationBool
    {
        public static bool ResourcesReservation(Dictionary<string, long> resourcesBuffer, StorageResources storageResources, PlayerReal player)
        {
            bool checkValue = false;
            foreach (var item in resourcesBuffer)
            {
                if (item.Key == "Money" && player.Ballance >= item.Value)
                {
                    checkValue = true;
                }
                else if (storageResources.Storage[item.Key] >= item.Value)
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
