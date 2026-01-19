using Tycoonia.Application.ApplicationExceptions;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ProductionCalculation
    {
        public static void ProductionCalculationFactory(StorageResources storageResources, FactoryBase factory, EnergyStorage energyStorage, PlayerReal player)
        {
            int fabrication;
            decimal energyNeeded;
            long playerMoney = player.Ballance;
            Dictionary<string, byte> receipeListNeeded = factory.ReceipeList;
            //factory.ResourceBuffer = [];

            foreach (var el in receipeListNeeded)
            {
                ResourcesSubtraction(storageResources, receipeListNeeded, player);
            }

            fabrication = factory.ProductionRate;

            energyNeeded = factory.EnergyConsumption;
            receipeListNeeded = currentFactory.ReceipeList;


            EnergyReservation(energyStorage, energyNeeded);
            SaveExtractionMine(storageResources, currentFactory, fabrication);
        }

        

        public static int ResourcesSubtraction(StorageResources storageResources, Dictionary<string, byte> receipeListNeeded, PlayerReal player)
        {
            long playerMoney = player.Ballance;

            foreach (var item in receipeListNeeded)
            {
                if (item.Key == "Money")
                {
                    player.Ballance -= (long)item.Value;
                }
                else if (storageResources.Storage[item.Key] >= item.Value)
                {
                    storageResources.Storage[item.Key] -= item.Value;
                }
                else
                {
                    throw new Exception("No meaning");
                }
            }
        }

        public static int EnergySubtraction(EnergyStorage energyStorage, decimal energyNeeded)
        {
            if (energyStorage.CurrentStorage >= energyNeeded)
            {
                energyStorage.CurrentStorage -= energyNeeded;
            }
            else
            {
                throw new StorageException();
            }
        }
    }
}
