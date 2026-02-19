using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class LaunchControleCenterFactory
    {
        public static void PreparationLaunchFactory(FactoryBase factory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player, int expectedOutput)
        {
            try
            {
                CreateBufferCheck(factory, storageResources, player, expectedOutput);
                bool checkValues = CheckingValuesForFactory(factory, storageResources, energyStorage, player);
                CreateProductionTime(factory, expectedOutput);
                if (!checkValues)
                {
                    throw new StorageException();
                }
                else
                {
                    BufferSubtraction(factory, storageResources, player);
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

        public static Dictionary<string, StorageResourcesBase> CreateBufferCheck(FactoryBase factory, StorageResources storageResources, PlayerReal player, int expectedOutput)
        {
            foreach (var item in factory.RecipeList)
            {
                factory.ResourceBuffer.Add(item.Key, new StorageResourcesBase { CurrentQuantity = item.Value * expectedOutput });
            }
            return factory.ResourceBuffer;
        }

        public static void BufferSubtraction(FactoryBase factory, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in factory.ResourceBuffer)
            {
                if (item.Key == "Money")
                {
                    player.SubtractSafe((long)item.Value.CurrentQuantity);
                }
                else
                {
                    storageResources.SubtractResourceSafe(item.Key, item.Value.CurrentQuantity);
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

        public static void CreateProductionTime(FactoryBase factory, int expectedOutput)
        {
            factory.ProductionTime = (decimal)expectedOutput/(decimal)factory.ProductionRate;
            factory.ProductionTimePerIteration = (decimal)factory.ProductionTime / (decimal)Math.Ceiling(factory.ProductionTime);
        }

        public static void StopFactory(FactoryBase factory, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in factory.ResourceBuffer)
            {
                if (item.Key == "Money")
                {
                    player.AddSafe(item.Value.CurrentQuantity);
                }
                else
                {
                    storageResources.AddResourceSafe(item.Key, item.Value.CurrentQuantity);
                }
            }
            factory.ProductionTime = 0;
            factory.ResourceBuffer.Clear();
            factory.WorkFlag = false;
        }
    }
}
