using Tycoonia.Application;
using Tycoonia.Application.Factory;
using Tycoonia.Application.Storage.Resources;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class FactoryOperations
    {
        private StorageResources storageResources;
        private Dictionary<string, StorageResourcesBase> startStorageResources;
        private FactoryBase factoryBricks;
        private List<FactoryBase> factories;
        private EnergyStorage energyStorage;
        private long startBallance;
        private PlayerReal player;

        private long startClay;
        private long startCoal;
        private long startUranium;
        private long startBricks;

        public FactoryOperations()
        {
            storageResources = new StorageResources();

            startClay = storageResources.StorageList["Clay"].CurrentQuantity;
            startCoal = storageResources.StorageList["Coal"].CurrentQuantity;
            startUranium = storageResources.StorageList["Uranium"].CurrentQuantity;
            startBricks = storageResources.StorageList["Bricks"].CurrentQuantity;

            startStorageResources = new Dictionary<string, StorageResourcesBase>(storageResources.StorageList);
            factoryBricks = new FactoryBricks();
            factories = [factoryBricks];
            energyStorage = new EnergyStorage();
            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void CorrectPreparationBufferAndSubtractionStorage()
        {
            LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);
            Dictionary<string, StorageResourcesBase> resultStorageResorces = storageResources.StorageList;
            long resultPlayerBallance = player.Ballance;
            Dictionary<string, StorageResourcesBase> resultFactoryResourceBuffer = factoryBricks.ResourceBuffer;

            // 52 Assert.Null(resultFactoryResourceBuffer);
            Assert.NotNull(resultFactoryResourceBuffer);

            // 55 Assert.Equal(startClay, resultStorageResorces["Clay"].CurrentQuantity);
            Assert.NotEqual(startClay, resultStorageResorces["Clay"].CurrentQuantity);

            // 58 Assert.NotEqual(startCoal, resultStorageResorces["Coal"].CurrentQuantity);
            Assert.Equal(startCoal, resultStorageResorces["Coal"].CurrentQuantity);

            // 61 Assert.NotEqual(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);
            Assert.Equal(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);

            // 64 Assert.NotEqual(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);
            Assert.Equal(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);

            // 67 Assert.Equal(startBallance, resultPlayerBallance);
            Assert.NotEqual(startBallance, resultPlayerBallance);

        }

        [Fact]
        public void CorrectPreparationLaunch()
        {
            LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);
            bool resultFactoryWorkFlag = factoryBricks.WorkFlag;

            Assert.True(resultFactoryWorkFlag);
        }

        [Fact]
        public void CorrectProductYield()
        {
            LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);
            int resultRate = ProductionCalculation.ProductionCalculationFactory(storageResources, factoryBricks, energyStorage);
            string productionItemKey = factories[0].ProductionItemList.Keys.First();
            long resultFactoryFabrication = storageResources.StorageList[productionItemKey].CurrentQuantity;
            decimal resultEnergyStorage = energyStorage.CurrentStorage;
            long resultBallance = player.Ballance;

            //91 Assert.Null(factoryBricks.ResourceBuffer);
            Assert.NotNull(factoryBricks.ResourceBuffer);

            //94 Assert.Equal(startBallance, resultBallance);
            Assert.NotEqual(startBallance, resultBallance);

            Assert.Equal(factoryBricks.ProductionRate, resultRate);

            Assert.Equal(2, resultFactoryFabrication);

            Assert.Equal(49999.5m, resultEnergyStorage);
        }

        [Fact]
        public void CorrectStopFactoryAndReturnInStorage()
        {
            LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);
            int resultRate = ProductionCalculation.ProductionCalculationFactory(storageResources, factoryBricks, energyStorage);
            LaunchControleCenterFactory.StopFactory(factoryBricks, storageResources, player);
            Dictionary<string, StorageResourcesBase> resultStorageResorces = storageResources.StorageList;
            long resultPlayerBallance = player.Ballance;
            Dictionary<string, StorageResourcesBase> resultFactoryResourceBuffer = factoryBricks.ResourceBuffer;

            Assert.Empty(resultFactoryResourceBuffer);

            // 116 Assert.Equal(startClay, resultStorageResorces["Clay"].CurrentQuantity);
            Assert.NotEqual(startClay, resultStorageResorces["Clay"].CurrentQuantity);

            // 119 Assert.NotEqual(startCoal, resultStorageResorces["Coal"].CurrentQuantity);
            Assert.Equal(startCoal, resultStorageResorces["Coal"].CurrentQuantity);

            // 122 Assert.NotEqual(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);
            Assert.Equal(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);

            // 125 Assert.Equal(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);
            Assert.NotEqual(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);

            // 128 Assert.Equal(startBallance, resultPlayerBallance);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }

        [Fact]
        public void CorrectCanUpgrade()
        {
            UpgradeBuilding.CanUpgrade(factoryBricks, storageResources, player);

            Assert.True(factoryBricks.CanUpgrade);
        }

        [Fact]
        public void CorrectUpgradeSubtraction()
        {
            UpgradeBuilding.UpgradeSubtraction(factoryBricks, storageResources, player);
            long resultStorageResourcesClay = storageResources.StorageList["Clay"].CurrentQuantity;
            long resultPlayerBallance = player.Ballance;

            // 148 Assert.NotEqual(startClay, resultStorageResourcesClay);
            Assert.NotEqual(startClay, resultStorageResourcesClay);

            // 151 Assert.Equal(startBallance, resultPlayerBallance);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }

        [Fact]
        public void CorrectUpgradeStorage()
        {
            UpgradeBuilding.Upgrade(factoryBricks, storageResources, player);
            long resultStorageResourcesClay = storageResources.StorageList["Clay"].CurrentQuantity;
            long resultPlayerBallance = player.Ballance;

            // 162 Assert.Equal(startClay, resultStorageResourcesClay);
            Assert.NotEqual(startClay, resultStorageResourcesClay);

            // 165 Assert.Equal(startBallance, resultPlayerBallance);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }
    }
}
