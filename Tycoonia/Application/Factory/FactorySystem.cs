using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class FactorySystem
    {
        public static int UpdateFactoryCalculations(StorageResources storageResources, FactoryBase factory, EnergyStorage energyStorage, PlayerReal player, int expectedOutput)
        {
            int result = ProductionCalculation.ProductionCalculationFactory(storageResources, factory, energyStorage);
            return result;
        }
    }
}
