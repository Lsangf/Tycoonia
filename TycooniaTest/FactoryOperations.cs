using Tycoonia.Application.Factory;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class FactoryOperations
    {
        private StorageResources storageResources;
        private Dictionary<string, long> startStorageResources;
        private FactoryBase factoryBricks;
        private List<FactoryBase> factories;
        private EnergyStorage energyStorage;
        private long startBallance;
        private PlayerReal player;

        public FactoryOperations()
        {
            storageResources = new StorageResources();
            startStorageResources = new Dictionary<string, long>(storageResources.Storage);
            factoryBricks = new FactoryBricks();
            factories = [factoryBricks];
            energyStorage = new EnergyStorage();
            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void FactoryCorrectPreparationBufferSubtractionStorage()
        {
            LaunchControleCenterFactory.CreateBufferCheck(factoryBricks, storageResources, player, 1);
            Dictionary<string, long> resultStorageResorces = storageResources.Storage;
            long resultPlayerBallance = player.Ballance;
            Dictionary<string, int> resultFactoryResourceBuffer = factoryBricks.ResourceBuffer;

            // 40 Assert.Null(resultFactoryResourceBuffer);
            Assert.NotNull(resultFactoryResourceBuffer);

            // 43 Assert.Equal(startStorageResources, resultStorageResorces);
            Assert.NotEqual(startStorageResources, resultStorageResorces);

            // 46 Assert.Equal(startBallance, resultPlayerBallance);
            Assert.NotEqual(startBallance, resultPlayerBallance);

        }

        [Fact]
        public void FactoryCorrectPreparationLaunch()
        {
            LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);
            bool resultFactoryWorkFlag = factoryBricks.WorkFlag;

            Assert.True(resultFactoryWorkFlag);
        }

        [Fact]
        public void FactoryCorrectProductYield()
        {
            LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);
            int resultRate = ProductionCalculation.ProductionCalculationFactory(storageResources, factoryBricks, energyStorage, player);
            long resultFactoryFabrication = storageResources.Storage[factories[0].ProductionItem];
            decimal resultEnergyStorage = energyStorage.CurrentStorage;
            long resultBallance = player.Ballance;

            //69 Assert.Null(factoryBricks.ResourceBuffer);
            Assert.NotNull(factoryBricks.ResourceBuffer);

            //72 Assert.Equal(startBallance, resultBallance);
            Assert.NotEqual(startBallance, resultBallance);

            Assert.Equal(factoryBricks.ProductionRate, resultRate);

            Assert.Equal(1, resultFactoryFabrication);

            Assert.Equal(49999.5m, resultEnergyStorage);
        }
    }
}
