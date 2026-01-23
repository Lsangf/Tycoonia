using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Mining
{
    public class ResourceExtraction
    {
        public static void ResourceExtractionMine(StorageResources storageResources, List<MineBase> mine, EnergyStorage energyStorage)
        {
            foreach (MineBase currentMine in mine)
            {
                EnergyReservation(energyStorage, currentMine);
                SaveExtractionMine(storageResources, currentMine);
            }
        }

        public static void EnergyReservation(EnergyStorage energyStorage, MineBase currentMine)
        {
            if (energyStorage.CurrentStorage >= currentMine.EnergyConsumption)
            {
                energyStorage.CurrentStorage -= currentMine.EnergyConsumption;
            }
            else
            {
                throw new StorageException();
            }
        }

        public static void SaveExtractionMine(StorageResources storageResources, MineBase currentMine)
        {
            if (storageResources.StorageList.ContainsKey(currentMine.ProductionItem))
            {
                storageResources.StorageList[currentMine.ProductionItem].CurrentQuantity += currentMine.ProductionRate;
            }
            else
            {
                throw new StorageException();
            }
        }
    }
}
