using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Energy
{
    public class EnergeticsCalculation
    {
        public static Dictionary<string, int> EnergeticsCalculationEnergyPlant(StorageResources storageResources, EnergyPlantBase energyPlant, EnergyStorage energyStorage, byte iteration)
        {
            decimal energyNeeded = energyPlant.EnergyConsumption;
            Dictionary<string, short> recipeListNeeded = energyPlant.RecipeList;
            Dictionary<string, StorageResourcesBase> resorcesBuffer = energyPlant.ResourceBuffer;

            ResourcesSubtraction(resorcesBuffer, recipeListNeeded);
            if (iteration == 0)
            {
                EnergySubtraction(energyStorage, energyNeeded);
                TimeSubtractionBuilding.TimeSubtraction(energyPlant);
                SaveInStorageEnergy.Save(storageResources, energyPlant, energyStorage);
            }
            else
            {
                TimeSubtractionBuilding.TimeSubtraction(energyPlant);
                SaveInStorageEnergy.Save(storageResources, energyPlant, energyStorage);
            }
            return energyPlant.ProductionItemList;
        }

        public static void ResourcesSubtraction(Dictionary<string, StorageResourcesBase> resorcesBuffer, Dictionary<string, short> recipeListNeeded)
        {
            foreach (var item in recipeListNeeded)
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
            energyStorage.SubtractSafe(energyNeeded);
        }
    }
}
