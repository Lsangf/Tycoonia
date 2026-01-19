using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class LaunchControleCenterFactory
    {
        public static void LaunchFactory(FactoryBase factory, StorageResources storageResources, EnergyStorage energyStorage, PlayerReal player)
        {
            try
            {
                if (!factory.WorkFlag)
                {
                    int expectedOutput = 1;
                    bool checkValues = CheckingValuesForFactory(storageResources, energyStorage, factory.ReceipeList, factory.EnergyConsumption, player, expectedOutput);
                    if (!checkValues)
                    {
                        throw new Exception("Not enough resources for production");
                    }
                    ProductionCalculation.ProductionCalculationFactory(storageResources, factory, energyStorage, player, expectedOutput);
                }
                else
                {
                    //ProductionCalculation.ProductionCalculationFactory();
                }
            }
            catch 
            { 
                factory.WorkFlag = false;
            }
        }
        public static bool CheckingValuesForFactory(StorageResources storageResources, EnergyStorage energyStorage, Dictionary<string, byte> receipeListNeeded, decimal energyNeeded, PlayerReal player, int expectedOutput)
        {
            bool checkResources = ReservationBool.ResourcesReservation(storageResources, receipeListNeeded, player);
            bool checkEnergy = ReservationBool.EnergyReservation(energyStorage, energyNeeded);
            if (checkResources && checkEnergy)
            {
                return true;
            }
            else
            {
                throw new Exception("Not enough energy/resources in storage.");
            }
        }

        public static void StopFactory()
        {

        }


    }
}
