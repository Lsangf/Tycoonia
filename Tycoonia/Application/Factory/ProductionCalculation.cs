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
            Dictionary<string, byte> recipeListNeeded = factory.RecipeList;
            Dictionary<string, StorageResourcesBase> resorcesBuffer = factory.ResourceBuffer;
            bool buferCheck = ResourcesBufferBool.CheckResourcesBuffer(resorcesBuffer, recipeListNeeded);

            if (!buferCheck)
            {
                factory.WorkFlag = false;
                return 0;
            }
            else
            {
                ResourcesSubtraction(resorcesBuffer, recipeListNeeded, factory);
                EnergySubtraction(energyStorage, energyNeeded, factory);
                TimeSubtractionBuilding.TimeSubtraction(factory);
                SaveInStorage.Save(storageResources, factory);
            }
            return factory.ProductionRate;
        }

        public static void ResourcesSubtraction(Dictionary<string, StorageResourcesBase> resorcesBuffer, Dictionary<string, byte> recipeListNeeded, FactoryBase factory)
        {
            foreach (var item in recipeListNeeded)
            {
                if (resorcesBuffer[item.Key].CurrentQuantity >= item.Value * factory.ProductionRate)
                {
                    resorcesBuffer[item.Key].CurrentQuantity -= item.Value * factory.ProductionRate;
                }
                else
                {
                    throw new StorageException("ERR resourcesCalculation");
                }
            }
        }

        public static void EnergySubtraction(EnergyStorage energyStorage, decimal energyNeeded, FactoryBase factory)
        {
            energyStorage.SubtractSafe(energyNeeded * factory.ProductionRate);
        }
    }
}
