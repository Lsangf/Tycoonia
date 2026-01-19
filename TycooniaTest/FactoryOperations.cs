using Tycoonia.Application.Factory;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
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
            StorageResources storageResources1 = new();
            FactoryBase factoryBricks = new FactoryBricks();
            List<FactoryBase> factories = [factoryBricks];
            EnergyStorage energyStorage = new();
            long startBallance = 10000;
            PlayerReal player = new("player1", startBallance);


            ProductionCalculation.ProductionCalculationFactory(storageResources, factories, energyStorage, player);
            long resultFactoryFabrication = storageResources.Storage[factories[0].ProductionItem];
            decimal resultEnergyStorage = energyStorage.CurrentStorage;
            long resultBallance = player.Ballance;

            Assert.NotEqual(startBallance, resultBallance);
            Assert.NotEqual(storageResources, storageResources1);
            Assert.Equal(1, resultFactoryFabrication);
            Assert.Equal(49999.5m, resultEnergyStorage);
        }
    }
}
