using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class LaunchControleCenterFactory
    {
        public static void LaunchFactory(FactoryBase factory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            try
            {
                int expectedOutput = 1;
                
                Dictionary<string, int> receipeListNeeded = CreateBufferCheck(factory, expectedOutput);

                bool checkValues = CheckingValuesForFactory(storageResources, energyStorage, receipeListNeeded, factory.EnergyConsumption, player, expectedOutput);
                if (!checkValues)
                {
                    throw new StorageException("Not enough resources for production");
                }
                factory.WorkFlag = true;

            }
            catch
            {
                // Log exception
                factory.ResourceBuffer.Clear();
                factory.WorkFlag = false;
            }
        }

        public static Dictionary<string, int> CreateBufferCheck(FactoryBase factory, int expectedOutput, StorageResources storageResources)
        {
            foreach (var item in factory.ReceipeList)
            {
                factory.ResourceBuffer.Add(item.Key, item.Value*expectedOutput);
            }
            foreach(var item in factory.ResourceBuffer)
            {

            }
            return factory.ResourceBuffer;
        }

        public static bool CheckingValuesForFactory(StorageResources storageResources, EnergyStorage energyStorage, Dictionary<string, int> receipeListNeeded, decimal energyNeeded, PlayerReal player, int expectedOutput)
        {
            bool checkResources = ReservationBool.ResourcesReservation(storageResources, receipeListNeeded, player);
            bool checkEnergy = ReservationBool.EnergyReservation(energyStorage, energyNeeded);
            if (checkResources && checkEnergy)
            {
                return true;
            }
            else
            {
                throw new Exception("Not enough energy/resources in storage.");
            }
        }

        public static void StopFactory()
        {

        }


    }
}
