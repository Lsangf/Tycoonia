using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ReservationBool
    {
        public static bool ResourcesReservation(Dictionary<string, int> resourcesBuffer, StorageResources storageResources, PlayerReal player)
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
