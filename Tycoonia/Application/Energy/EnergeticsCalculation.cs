using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Application.Storage;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Energy
{
    public class EnergeticsCalculation
    {
        public static Dictionary<string, int> EnergeticsCalculationEnergyPlant(StorageResources storageResources, EnergyPlantBase energyPlant, EnergyStorage energyStorage)
        {
            decimal energyNeeded = energyPlant.EnergyConsumption;
            Dictionary<string, short> receipeListNeeded = energyPlant.ReceipeList;
            Dictionary<string, StorageResourcesBase> resorcesBuffer = energyPlant.ResourceBuffer;

            ResourcesSubtraction(resorcesBuffer, receipeListNeeded);
            EnergySubtraction(energyStorage, energyNeeded);
            SaveInStorageEnergy.Save(storageResources, energyPlant, energyStorage);

            return energyPlant.ProductionItemList;
        }

        public static void ResourcesSubtraction(Dictionary<string, StorageResourcesBase> resorcesBuffer, Dictionary<string, short> receipeListNeeded)
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
