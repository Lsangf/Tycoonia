using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class LaunchControleCenterFactory
    {
        public static void PreparationLaunchFactory(FactoryBase factory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player/*,  int expectedOutput*/)
        {
            try
            {
                int expectedOutput = 1;
                CreateBufferCheck(factory, storageResources, player, expectedOutput);

                bool checkValues = CheckingValuesForFactory(factory, storageResources, energyStorage, player);
                if (!checkValues)
                {
                    throw new StorageException();
                }
                else
                {
                    factory.WorkFlag = true;
                }
            }
            catch
            {
                // Log exception
                factory.ResourceBuffer.Clear();
                factory.WorkFlag = false;
            }
        }

        public static Dictionary<string, long> CreateBufferCheck(FactoryBase factory, StorageResources storageResources, PlayerReal player,  int expectedOutput)
        {
            foreach (var item in factory.ReceipeList)
            {
                factory.ResourceBuffer.Add(item.Key, item.Value*expectedOutput);
            }
            BufferSubtraction(factory, storageResources, player);
            return factory.ResourceBuffer;
        }

        public static void BufferSubtraction(FactoryBase factory, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in factory.ResourceBuffer)
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

        public static bool CheckingValuesForFactory(FactoryBase factory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            bool checkResources = ReservationBool.ResourcesReservation(factory.ResourceBuffer, storageResources, player);
            bool checkEnergy = ReservationBool.EnergyReservation(factory.EnergyConsumption, energyStorage);
            if (checkResources && checkEnergy)
            {
                return true;
            }
            else
            {
                throw new StorageException();
            }
        }

        public static void StopFactory(FactoryBase factory, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in factory.ResourceBuffer)
            {
                if (item.Key == "Money")
                {
                    player.Ballance += item.Value;
                }
                else if (storageResources.StorageList.ContainsKey(item.Key))
                {
                    storageResources.StorageList[item.Key] += item.Value;
                }
                else
                {
                    throw new StorageException();
                }
            }
            factory.ResourceBuffer.Clear();
        }
    }
}
