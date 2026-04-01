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
                //CreateProductionTime(factory, expectedOutput);
                StartFactoryProduction.Start(factory, expectedOutput);
                if (!checkValues)
                {
                    throw new StorageException();
                }
                else
                {
                    BufferSubtraction(factory, storageResources, player);
                    factory.LastUpdateTime = DateTime.UtcNow;
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
            if (factory.ResourceBuffer.Count == 0)
            {
                foreach (var item in factory.RecipeList)
                {
                    factory.ResourceBuffer.Add(item.Key, new StorageResourcesBase { CurrentQuantity = item.Value * expectedOutput });
                }
            }
            else
            {
                foreach (var item in factory.ResourceBuffer)
                {
                    if (item.Value.CurrentQuantity == 0)
                    {
                        factory.ResourceBuffer[item.Key].CurrentQuantity = factory.RecipeList[item.Key] * expectedOutput;
                    }
                }
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

        public static class StartFactoryProduction
        {
            public static void Start(FactoryBase factory, decimal expectedOutput)
            {
                factory.TargetOutput = expectedOutput;
                factory.Produced = 0;

                factory.LastUpdateTime = DateTime.UtcNow;
                factory.WorkFlag = true;
            }
        }

        public static void StopFactory(FactoryBase factory, StorageResources storageResources, PlayerReal player)
        {
            foreach (var item in factory.ResourceBuffer)
            {
                if (item.Key == "Money")
                {
                    player.AddSafe((long)Math.Floor(item.Value.CurrentQuantity));
                }
                else
                {
                    storageResources.AddResourceSafe(item.Key, item.Value.CurrentQuantity);
                }
            }
            factory.ResourceBuffer.Clear();
            factory.WorkFlag = false;
        }
    }
}
