using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ProductionCalculation
    {
        public static int ProductionCalculationFactory(StorageResources storageResources, FactoryBase factory, EnergyStorage energyStorage)
        {
            decimal energyNeeded = factory.EnergyConsumption;
            Dictionary<string, byte> receipeListNeeded = factory.RecipeList;
            Dictionary<string, StorageResourcesBase> resorcesBuffer = factory.ResourceBuffer;
            bool buferCheck = ResourcesBufferBool.CheckResourcesBuffer(resorcesBuffer, receipeListNeeded);

            if (!buferCheck)
            {
                factory.WorkFlag = false;
                return 0;
            }
            else
            {
                ResourcesSubtraction(resorcesBuffer, receipeListNeeded);
                EnergySubtraction(energyStorage, energyNeeded);
                SaveInStorage.Save(storageResources, factory);
            }
            return factory.ProductionRate;
        }

        public static void ResourcesSubtraction(Dictionary<string, StorageResourcesBase> resorcesBuffer, Dictionary<string, byte> receipeListNeeded)
        {
            foreach (var item in receipeListNeeded)
            {
                if (resorcesBuffer[item.Key].CurrentQuantity >= item.Value)
                {
                    resorcesBuffer[item.Key].CurrentQuantity -= item.Value;
                }
                else
                {
                    throw new StorageException("ERR resourcesCalculation");
                }
            }
        }

        public static void EnergySubtraction(EnergyStorage energyStorage, decimal energyNeeded)
        {
            if (energyStorage.CurrentStorage >= energyNeeded)
            {
                energyStorage.CurrentStorage -= energyNeeded;
            }
            else
            {
                throw new StorageException("ERR energyCalculation");
            }
        }
    }
}
