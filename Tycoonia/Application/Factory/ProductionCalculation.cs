using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ProductionCalculation
    {
        public static void ProductionCalculationFactory(StorageResources storageResources, List<FactoryBase> factoryList, EnergyStorage energyStorage, PlayerReal player)
        {
            int fabrication;
            decimal energyNeeded;
            long playerMoney = player.Ballance;
            Dictionary<string, byte> receipeListNeeded = [];

            foreach (FactoryBase currentFactory in factoryList)
            {
                fabrication = currentFactory.ProductionRate;

                energyNeeded = currentFactory.EnergyConsumption;
                receipeListNeeded = currentFactory.ReceipeList;

                ResourcesReservation(storageResources, receipeListNeeded, playerMoney);
                EnergyReservation(energyStorage, energyNeeded);
                SaveExtractionMine(storageResources, currentFactory, fabrication);
            }
        }

        public static void ResourcesReservation(StorageResources storageResources, Dictionary<string, byte> receipeListNeeded, long playerMoney)
        {
            foreach (var item in receipeListNeeded)
            {
                if (item.Key == "Money" && playerMoney >= item.Value)
                {
                    playerMoney -= (long)item.Value;
                }
                else if (storageResources.Storage[item.Key] >= item.Value && item.Key != "Money")
                {
                    storageResources.Storage[item.Key] -= item.Value;
                }
                else
                {
                    throw new Exception("Not enough resources in storage.");
                }
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

        public static void SaveExtractionMine(StorageResources storageResources, FactoryBase currentFactory, int fabrication)
        {
            if (storageResources.Storage.ContainsKey(currentFactory.ProductionItem))
            {
                storageResources.Storage[currentFactory.ProductionItem] += fabrication;
            }
            else
            {
                throw new Exception("Resource type not found in storage.");
            }
        }





    }
}
