using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class LaunchControleCenterFactory
    {
        public static bool PreparationLaunchFactory(FactoryBase factory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            try
            {
                int expectedOutput = 1;
                
                Dictionary<string, int> receipeListNeeded = CreateBufferCheck(factory, expectedOutput);

                bool checkValues = CheckingValuesForFactory(factory, storageResources, energyStorage, player);
                if (!checkValues)
                {
                    throw new StorageException();
                }
                return true;
            }
            catch
            {
                // Log exception
                factory.ResourceBuffer.Clear();
                return false;
            }
        }

        public static Dictionary<string, int> CreateBufferCheck(FactoryBase factory, int expectedOutput)
        {
            foreach (var item in factory.ReceipeList)
            {
                factory.ResourceBuffer.Add(item.Key, item.Value*expectedOutput);
            }
            return factory.ResourceBuffer;
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

        public static void StopFactory()
        {

        }
    }
}
