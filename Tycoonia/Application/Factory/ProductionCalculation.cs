using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ProductionCalculation
    {
        public static int ProductionCalculationFactory(StorageResources storageResources, FactoryBase factory, EnergyStorage energyStorage)
        {
            // long playerMoney = player.Ballance; // for the transaction
            decimal energyNeeded = factory.EnergyConsumption;
            Dictionary<string, byte> receipeListNeeded = factory.ReceipeList;
            Dictionary<string, long> resorcesBuffer = factory.ResourceBuffer;
            
            ResourcesSubtraction(resorcesBuffer, receipeListNeeded);
            EnergySubtraction(energyStorage, energyNeeded);
            SaveInStorage.Save(storageResources, factory);

            return factory.ProductionRate;
        }

        public static void ResourcesSubtraction(Dictionary<string, long> resorcesBuffer, Dictionary<string, byte> receipeListNeeded)
        {
            foreach (var item in receipeListNeeded)
            {
                if (resorcesBuffer[item.Key] >= item.Value)
                {
                    resorcesBuffer[item.Key] -= item.Value;
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
