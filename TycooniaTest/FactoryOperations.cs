using Tycoonia.Application.Factory;
using Tycoonia.Application.Mining;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class FactoryOperations
    {
        [Fact]
        public void FactoryOperationsExtraction()
        {
            StorageResources storageResources = new();
            FactoryBase factoryBricks = new FactoryBricks();
            List<FactoryBase> factories = [factoryBricks];
            EnergyStorage energyStorage = new();
            PlayerReal player = new("player1", 10000);


            ProductionCalculation.ProductionCalculationFactory(storageResources, factories, energyStorage, player);
            long resultFactoryFabrication = storageResources.Storage[factories[0].ProductionItem];
            decimal resultEnergyStorage = energyStorage.CurrentStorage;

            Assert.Equal(1, resultFactoryFabrication);
            Assert.Equal(49999.5m, resultEnergyStorage);
        }
    }
}
