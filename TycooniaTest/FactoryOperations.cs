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
        private FactoryBase factoryAluminum;
        private FactoryBase factoryBricks;
        private FactoryBase factoryBatteries;
        private FactoryBase factoryConcrete;
        private FactoryBase factoryCopperWire;
        private FactoryBase factoryDiamonds;
        private FactoryBase factoryElectronicComponents;
        private FactoryBase factoryEnergyStorage;
        private FactoryBase factoryEnrichmentUranium;
        private FactoryBase factoryGlass;
        private FactoryBase factoryGoldBars;
        private FactoryBase factoryPurifiedLithium;
        private FactoryBase factoryFuel;
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
        private int startProductionTimeFactory;
        private EnergyStorage energyStorage;
        private EnergyStorage startEnergyStorage;
        private long startBallance;
        private PlayerReal player;

        public FactoryOperations()
        {
            startStorageResources = new StorageResources();
            storageResources = new StorageResources();

            factoryAluminum = new FactoryAluminum();
            factoryBricks = new FactoryBricks();
            factoryBatteries = new FactoryBatteries();
            factoryConcrete = new FactoryConcrete();
            factoryCopperWire = new FactoryCopperWire();
            factoryDiamonds = new FactoryDiamonds();
            factoryElectronicComponents = new FactoryElectronicComponents();
            factoryEnergyStorage = new FactoryEnergyStorage();
            factoryEnrichmentUranium = new FactoryEnrichmentUranium();
            factoryGlass = new FactoryGlass();
            factoryGoldBars = new FactoryGoldBars();
            factoryPurifiedLithium = new FactoryPurifiedLithium();
            factoryFuel = new FactoryFuel();
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
                factoryAluminum, factoryBricks, factoryBatteries, factoryConcrete, 
                factoryCopperWire, factoryDiamonds, factoryElectronicComponents, factoryEnergyStorage, 
                factoryEnrichmentUranium, factoryGlass, factoryGoldBars, factoryPurifiedLithium, 
                factoryFuel, factoryPlastic, factorySilicon, factorySilverBars, 
                factorySolidFuel, factorySteel, factoryThoriumRod, factoryTitanium, 
                factoryUraniumRod
                ];
            startProductionTimeFactory = 10;

            energyStorage = new EnergyStorage();
            startEnergyStorage = new EnergyStorage();

            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void CorrectPreparationBufferAndSubtractionStorage()
        {
            foreach (var factory in factories)
            {
                LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player, 1);

                // 97 Assert.Null(factory.ResourceBuffer);
                Assert.NotNull(factory.ResourceBuffer);

                // 100 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
                Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);

                foreach (var item in factory.RecipeList)
                {
                    if (item.Key == "Money")
                    {
                        // 107 Assert.Equal(startBallance, player.Ballance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 112 Assert.Equal(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                    }
                }
                Assert.True(factory.WorkFlag);
            }
        }

        [Fact]
        public void CorrectPreparationLaunch()
        {
            foreach (var factory in factories)
            {
                LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player, 1);
                Assert.True(factory.WorkFlag);
            }
        }

        [Fact]
        public void CorrectProductYield()
        {
            foreach (var factory in factories)
            {
                factory.ProductionTime = startProductionTimeFactory;
                LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player, 1);
                int resultRate = ProductionCalculation.ProductionCalculationFactory(storageResources, factory, energyStorage);
                int resultProductionTimeFactory = factory.ProductionTime;

                // 140 Assert.Null(factory.ResourceBuffer);
                Assert.NotNull(factory.ResourceBuffer);

                Assert.Equal(factory.ProductionRate, resultRate);

                // 145 Assert.Equal(startProductionTimeFactory, resultProductionTimeFactory);
                Assert.NotEqual(startProductionTimeFactory, resultProductionTimeFactory);
            }
            // 148 Assert.Equal(startBallance, player.Ballance);
            Assert.NotEqual(startBallance, player.Ballance);

            // 151 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
            Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);

            // 154 Assert.Equal(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
            Assert.NotEqual(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
        }

        [Fact]
        public void CorrectStopFactoryAndReturnInStorage()
        {
            foreach (var factory in factories)
            {
                factory.ProductionTime = startProductionTimeFactory;
                LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player, 1);
                int resultRate = ProductionCalculation.ProductionCalculationFactory(storageResources, factory, energyStorage);
                int resultProductionTimeFactory = factory.ProductionTime;
                LaunchControleCenterFactory.StopFactory(factory, storageResources, player);

                Assert.Empty(factory.ResourceBuffer);
                Assert.Equal(factory.ProductionRate, resultRate);
                Assert.Equal(0, resultProductionTimeFactory);
            }
            // 173 Assert.Equal(startBallance, player.Ballance);
            Assert.NotEqual(startBallance, player.Ballance);

            // 176 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
            Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);

            // 179 Assert.Equal(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
            Assert.NotEqual(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
        }

        [Fact]
        public void CorrectSubtractionTime()
        {
            foreach (var factory in factories)
            {
                factory.ProductionTime = startProductionTimeFactory;
                TimeSubtractionBuilding.TimeSubtraction(factory);
                int resultProductionTimeFactory = factory.ProductionTime;

                // 192 Assert.Equal(startProductionTimeFactory, resultProductionTimeFactory);
                Assert.NotEqual(startProductionTimeFactory, resultProductionTimeFactory);
            }
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
                        // 217 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 222 Assert.Equal(startClay, resultStorageResourcesProductionItem);
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
                        // 239 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                        Assert.Equal(200, factory.ReceipeUpgradeList["Money"]);
                    }
                    else
                    {
                        // 245 Assert.Equal(startStorageResorces.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.Equal(2, factory.ReceipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(factory.CanUpgrade);
                Assert.Equal(2, factory.Level);
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
                        Assert.Equal(200, factory.ReceipeUpgradeList["Money"]);
                    }
                    else
                    {
                        Assert.Equal(2, factory.ReceipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(factory.CanUpgrade);
                Assert.Equal(2, factory.Level);
            }
        }
    }
}
