using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Mining
{
    public class ResourceExtraction
    {
        public static void ResourceExtractionMine(StorageResources storageResources, List<MineBase> mine, EnergyStorage energyStorage)
        {
            int mining;
            decimal energyNeeded;
            foreach (MineBase currentMine in mine)
            {
                mining = currentMine.ProductionRate;
                energyNeeded = currentMine.EnergyConsumption;

                EnergyReservation(energyStorage, energyNeeded);
                SaveExtractionMine(storageResources, currentMine, mining);
            }
        }

        public static void EnergyReservation(EnergyStorage energyStorage, decimal energyNeeded)
        {
            if (energyStorage.CurrentStorage >= energyNeeded)
            {
                energyStorage.CurrentStorage -= energyNeeded;
            }
            else
            {
                throw new Exception("Not enough energy in storage.");
            }
        }

        public static void SaveExtractionMine(StorageResources storageResources, MineBase currentMine, int mining)
        {
            if (storageResources.Storage.ContainsKey(currentMine.Name))
            {
                storageResources.Storage[currentMine.Name] += mining;
            }
            else
            {
                throw new Exception("Resource type not found in storage.");
            }
        }
    }
}
