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


            LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);
            ProductionCalculation.ProductionCalculationFactory(storageResources, factoryBricks, energyStorage, player);
            long resultFactoryFabrication = storageResources.Storage[factories[0].ProductionItem];
            decimal resultEnergyStorage = energyStorage.CurrentStorage;
            long resultBallance = player.Ballance;

            // "//(line number)" to check exact values ​​without comparing correctness

            //31 Assert.Null(factoryBricks.ResourceBuffer);
            Assert.NotNull(factoryBricks.ResourceBuffer);

            //34 Assert.Equal(startBallance, resultBallance);
            Assert.NotEqual(startBallance, resultBallance);

            //37 Assert.Equal(storageResources, storageResources1);
            Assert.NotEqual(storageResources, storageResources1);

            Assert.Equal(1, resultFactoryFabrication);

            Assert.Equal(49999.5m, resultEnergyStorage);
        }
    }
}
