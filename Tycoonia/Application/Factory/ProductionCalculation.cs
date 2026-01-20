using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ProductionCalculation
    {
        public static int ProductionCalculationFactory(StorageResources storageResources, FactoryBase factory, EnergyStorage energyStorage, PlayerReal player)
        {
            // long playerMoney = player.Ballance; // for the transaction
            decimal energyNeeded = factory.EnergyConsumption;
            Dictionary<string, byte> receipeListNeeded = factory.ReceipeList;
            Dictionary<string, int> resorcesBuffer = factory.ResourceBuffer;
            
            ResourcesSubtraction(resorcesBuffer, receipeListNeeded, player);
            EnergySubtraction(energyStorage, energyNeeded);
            SaveInStorage.Save(storageResources, factory);

            return factory.ProductionRate;
        }

        public static void ResourcesSubtraction(Dictionary<string, int> resorcesBuffer, Dictionary<string, byte> receipeListNeeded, PlayerReal player)
        {
            foreach (var item in receipeListNeeded)
            {
                if (item.Key == "Money")
                {
                    resorcesBuffer[item.Key] -= item.Value;
                }
                else if (resorcesBuffer[item.Key] >= item.Value)
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
