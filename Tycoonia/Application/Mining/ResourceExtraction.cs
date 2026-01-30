using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Mining
{
    public class ResourceExtraction
    {
        public static void ResourceExtractionMine(StorageResources storageResources, MineBase mine, EnergyStorage energyStorage)
        {
            EnergyReservation(energyStorage, mine);
            SaveExtractionMine(storageResources, mine);
        }

        public static void EnergyReservation(EnergyStorage energyStorage, MineBase mine)
        {
            if (energyStorage.CurrentStorage >= mine.EnergyConsumption)
            {
                energyStorage.CurrentStorage -= mine.EnergyConsumption;
            }
            else
            {
                throw new StorageException();
            }
        }

        public static void SaveExtractionMine(StorageResources storageResources, MineBase mine)
        {
            if (storageResources.StorageList.ContainsKey(mine.ProductionItem))
            {
                storageResources.StorageList[mine.ProductionItem].CurrentQuantity += mine.ProductionRate;
            }
            else
            {
                throw new StorageException();
            }
        }
    }
}
