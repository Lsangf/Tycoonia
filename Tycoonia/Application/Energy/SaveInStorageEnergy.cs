using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Energy
{
    public class SaveInStorageEnergy
    {
        public static void Save(StorageResources storageResources, EnergyPlantBase energyPlant, EnergyStorage energyStorage)
        {
            foreach (var item in energyPlant.ProductionItemList)
            {
                if (item.Key == "Energy")
                {
                    energyStorage.CurrentStorage += item.Value;
                }
                else if (storageResources.StorageList.ContainsKey(item.Key))
                {
                    storageResources.StorageList[item.Key].CurrentQuantity += item.Value;
                }
                else
                {
                    throw new StorageException();
                }
            }
        }
    }
}
