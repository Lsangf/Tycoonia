using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ReservationBool
    {
        public static bool ResourcesReservation(StorageResources storageResources, Dictionary<string, byte> receipeListNeeded, PlayerReal player)
        {
            bool checkValue = false;
            foreach (var item in receipeListNeeded)
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

        public static bool EnergyReservation(EnergyStorage energyStorage, decimal energyNeeded)
        {
            if (energyStorage.CurrentStorage >= energyNeeded)
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
