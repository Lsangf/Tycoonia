using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Energy
{
    public class LaunchControleCenterEnergyPlant
    {
        public static void PreparationLaunchEnergyPlant(EnergyPlantBase energyPlant, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player, int expectedOutput)
        {
            try
            {
                CreateBufferCheck(energyPlant, storageResources, expectedOutput);
                bool checkValues = CheckingValuesForFactory(energyPlant, storageResources, energyStorage, player);
				CreateProductionTime(energyPlant, expectedOutput);
				if (!checkValues)
                {
                    throw new StorageException();
                }
                else
                {
                    BufferSubtraction(energyPlant, storageResources);
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

        public static Dictionary<string, StorageResourcesBase> CreateBufferCheck(EnergyPlantBase energyPlant, StorageResources storageResources, int expectedOutput)
        {
            foreach (var item in energyPlant.RecipeList)
            {
                energyPlant.ResourceBuffer.Add(item.Key, new StorageResourcesBase { CurrentQuantity = item.Value * expectedOutput });
            }
            return energyPlant.ResourceBuffer;
        }

        public static void BufferSubtraction(EnergyPlantBase energyPlant, StorageResources storageResources)
        {
            foreach (var item in energyPlant.ResourceBuffer)
            {
                storageResources.SubtractResourceSafe(item.Key, item.Value.CurrentQuantity);
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

		public static void CreateProductionTime(EnergyPlantBase energyPlant, int expectedOutput)
		{
			energyPlant.ProductionTime = (decimal)expectedOutput / (decimal)energyPlant.ProductionRate;
            energyPlant.ProductionTimePerIteration = (decimal)energyPlant.ProductionTime / (decimal)Math.Ceiling(energyPlant.ProductionTime);
        }
	}
}
