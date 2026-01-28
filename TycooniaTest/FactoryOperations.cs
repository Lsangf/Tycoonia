using Tycoonia.Application;
using Tycoonia.Application.Factory;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class FactoryOperations
    {
        private StorageResources storageResources;
        private FactoryBase factoryBricks;
        private FactoryBase factoryBatteries;
        private FactoryBase factoryConcrete;
        private FactoryBase factoryDiamonds;
        private FactoryBase factoryElectronicComponent;
        private FactoryBase factoryEnrichmentUranium;
        private FactoryBase factoryGlass;
        private FactoryBase factoryGoldBars;
        private FactoryBase factoryLithium;
        private FactoryBase factoryOil;
        private FactoryBase factoryPlastic;
        private FactoryBase factorySilicon;
        private FactoryBase factorySilverBars;
        private FactoryBase factorySolidFuel;
        private FactoryBase factorySteel;
        private FactoryBase factoryThoriumRod;
        private FactoryBase factoryTitanium;
        private FactoryBase factoryUraniumRod;

        private StorageResources startStorageResources;
        private List<FactoryBase> factories;
        private EnergyStorage energyStorage;
        private long startBallance;
        private PlayerReal player;

        public FactoryOperations()
        {
            startStorageResources = new StorageResources();
            storageResources = new StorageResources();

            factoryBricks = new FactoryBricks();
            factoryBatteries = new FactoryBatteries();
            factoryConcrete = new FactoryConcrete();
            factoryDiamonds = new FactoryDiamonds();
            factoryElectronicComponent = new FactoryElectronicComponents();
            factoryEnrichmentUranium = new FactoryEnrichmentUranium();
            factoryGlass = new FactoryGlass();
            factoryGoldBars = new FactoryGoldBars();
            factoryLithium = new FactoryLithium();
            factoryOil = new FactoryOil();
            factoryPlastic = new FactoryPlastic();
            factorySilicon = new FactorySilicon();
            factorySilverBars = new FactorySilverBars();
            factorySolidFuel = new FactorySolidFuel();
            factorySteel = new FactorySteel();
            factoryThoriumRod = new FactoryThoriumRod();
            factoryTitanium = new FactoryTitanium();
            factoryUraniumRod = new FactoryUraniumRod();


            factories = 
                [
                factoryBricks, factoryBatteries, factoryConcrete, factoryDiamonds,
                factoryElectronicComponent, factoryEnrichmentUranium, factoryGlass, factoryGoldBars,
                factoryLithium, factoryOil, factoryPlastic, factorySilicon, factorySilverBars,
                factorySolidFuel, factorySteel, factoryThoriumRod, factoryTitanium, factoryUraniumRod
                ];

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
            Assert.NotEqual(startStorageResources.StorageList["Clay"].CurrentQuantity, resultStorageResorces["Clay"].CurrentQuantity);

            // 58 Assert.NotEqual(startCoal, resultStorageResorces["Coal"].CurrentQuantity);
            Assert.Equal(startStorageResources.StorageList["Coal"].CurrentQuantity, resultStorageResorces["Coal"].CurrentQuantity);

            // 61 Assert.NotEqual(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);
            Assert.Equal(startStorageResources.StorageList["Uranium"].CurrentQuantity, resultStorageResorces["Uranium"].CurrentQuantity);

            // 64 Assert.NotEqual(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);
            Assert.Equal(startStorageResources.StorageList["Bricks"].CurrentQuantity, resultStorageResorces["Bricks"].CurrentQuantity);

            // 67 Assert.Equal(startBallance, resultPlayerBallance);
            Assert.NotEqual(startBallance, resultPlayerBallance);

        }

        //[Fact] //When all resources are added to the storage, enable the test
        //public void CorrectPreparationLaunch()
        //{
        //    foreach (var factory in factories)
        //    {
        //        LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player);
        //        bool resultFactoryWorkFlag = factory.WorkFlag;
        //        Assert.False(resultFactoryWorkFlag); //When all resources are added to the storage, replace False with True
        //    }
        //}

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

            Assert.Equal(51, resultFactoryFabrication);

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
            Assert.NotEqual(startStorageResources.StorageList["Clay"].CurrentQuantity, resultStorageResorces["Clay"].CurrentQuantity);

            // 119 Assert.NotEqual(startCoal, resultStorageResorces["Coal"].CurrentQuantity);
            Assert.Equal(startStorageResources.StorageList["Coal"].CurrentQuantity, resultStorageResorces["Coal"].CurrentQuantity);

            // 122 Assert.NotEqual(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);
            Assert.Equal(startStorageResources.StorageList["Uranium"].CurrentQuantity, resultStorageResorces["Uranium"].CurrentQuantity);

            // 125 Assert.Equal(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);
            Assert.NotEqual(startStorageResources.StorageList["Bricks"].CurrentQuantity, resultStorageResorces["Bricks"].CurrentQuantity);

            // 128 Assert.Equal(startBallance, resultPlayerBallance);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }

        [Fact]
        public void CorrectCanUpgrade()
        {
            foreach (var factory in factories)
            {
                UpgradeBuilding.CanUpgrade(factory, storageResources, player);
                Assert.True(factory.CanUpgrade);
            }
        }

        [Fact]
        public void CorrectUpgradeSubtraction()
        {
            foreach (var factory in factories)
            {
                UpgradeBuilding.UpgradeSubtraction(factory, storageResources, player);
                foreach (var item in factory.ReceipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 151 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 148 Assert.Equal(startClay, resultStorageResourcesProductionItem);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                    }
                }
            }
        }

        [Fact]
        public void CorrectUpgradeStorage()
        {
            foreach (var factory in factories)
            {
                UpgradeBuilding.Upgrade(factory, storageResources, player);
                foreach (var item in factory.ReceipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 151 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                        Assert.Equal(200, factoryBricks.ReceipeUpgradeList["Money"]);
                    }
                    else
                    {
                        // 148 Assert.Equal(startStorageResorces.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.Equal(2, factory.ReceipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(factoryBricks.CanUpgrade);
                Assert.Equal(2, factoryBricks.Level);
            }
        }

        [Fact]
        public void CorrectUpdateUpgradeAmount()
        {
            foreach (var factory in factories)
            {
                UpgradeBuilding.UpdateUpgradeAmount(factory);
                foreach (var item in factory.ReceipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        Assert.Equal(200, factoryBricks.ReceipeUpgradeList["Money"]);
                    }
                    else
                    {
                        Assert.Equal(2, factory.ReceipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(factoryBricks.CanUpgrade);
                Assert.Equal(2, factoryBricks.Level);
            }
        }
    }
}
