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
            energyStorage.SubtractSafe(mine.EnergyConsumption);
        }

        public static void SaveExtractionMine(StorageResources storageResources, MineBase mine)
        {
            storageResources.AddResourceSafe(mine.ProductionItem, mine.ProductionRate);
        }
    }
}
