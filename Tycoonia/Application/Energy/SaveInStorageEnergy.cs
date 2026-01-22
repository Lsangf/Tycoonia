using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Energy
{
    public class SaveInStorageEnergy
    {
        public static void Save(StorageResources storageResources, EnergyPlantBase energyPlant)
        {
            foreach (var item in energyPlant.ProductionItemList)
            {
                if (storageResources.StorageList.ContainsKey(item.Key))
                {
                    storageResources.StorageList[item.Key] += item.Value;
                }
                else
                {
                    throw new StorageException();
                }
            }
        }
    }
}
