using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Energy
{
    public class LaunchControleCenterEnergyPlant
    {
        public static void PreparationLaunchEnergyPlant(EnergyPlantBase energyPlant, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player/*,  int expectedOutput*/)
        {
            try
            {
                int expectedOutput = 1;
                CreateBufferCheck(energyPlant, storageResources, player, expectedOutput);

                bool checkValues = CheckingValuesForFactory(energyPlant, storageResources, energyStorage, player);
                if (!checkValues)
                {
                    throw new StorageException();
                }
                else
                {
                    energyPlant.WorkFlag = true;
                }
            }
            catch
            {
                // Log exception
                energyPlant.ResourceBuffer.Clear();
                energyPlant.WorkFlag = false;
            }
        }

        public static Dictionary<string, long> CreateBufferCheck(EnergyPlantBase energyPlant, StorageResources storageResources, PlayerReal player, int expectedOutput)
        {
            foreach (var item in energyPlant.ReceipeList)
            {
                energyPlant.ResourceBuffer.Add(item.Key, item.Value * expectedOutput);
            }
            BufferSubtraction(energyPlant, storageResources, player);
            return energyPlant.ResourceBuffer;
        }

        public static void BufferSubtraction(EnergyPlantBase energyPlant, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in energyPlant.ResourceBuffer)
            {
                if (item.Key == "Money")
                {
                    player.Ballance -= (long)item.Value;
                }
                else if (storageResources.StorageList[item.Key] >= item.Value)
                {
                    storageResources.StorageList[item.Key] -= item.Value;
                }
                else
                {
                    throw new StorageException("ERR bufferCalculation");
                }
            }
        }

        public static bool CheckingValuesForFactory(EnergyPlantBase energyPlant, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            bool checkResources = ReservationBool.ResourcesReservation(energyPlant.ResourceBuffer, storageResources, player);
            bool checkEnergy = ReservationBool.EnergyReservation(energyPlant.EnergyConsumption, energyStorage);
            if (checkResources && checkEnergy)
            {
                return true;
            }
            else
            {
                throw new StorageException();
            }
        }

    }
}
