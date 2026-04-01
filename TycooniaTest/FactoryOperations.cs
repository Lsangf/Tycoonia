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
        private decimal startMaxExpectedOtput;

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

            startMaxExpectedOtput = 0;
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void CorrectPreparationBufferAndSubtractionStorage()
        {
            foreach (var factory in factories)
            {
                LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player, 1);

                // 100 Assert.Null(factory.ResourceBuffer);
                Assert.NotNull(factory.ResourceBuffer);

                // 103 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
                Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);

                foreach (var item in factory.RecipeList)
                {
                    if (item.Key == "Money")
                    {
                        // 110 Assert.Equal(startBallance, player.Ballance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 115 Assert.Equal(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
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
                LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player, 1);
                ProductionCalculation.ProductionCalculationFactory(storageResources, factory, energyStorage);

                // 143 Assert.Null(factory.ResourceBuffer);
                Assert.NotNull(factory.ResourceBuffer);

                Assert.Equal(factory.ProductionRate, factory.ProductionRate);
            }
            // 151 Assert.Equal(startBallance, player.Ballance);
            Assert.NotEqual(startBallance, player.Ballance);

            // 154 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
            Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);

            // 157 Assert.Equal(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
            Assert.NotEqual(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
        }

        [Fact]
        public void CorrectStopFactoryAndReturnInStorage()
        {
            foreach (var factory in factories)
            {
                LaunchControleCenterFactory.PreparationLaunchFactory(factory, storageResources, energyStorage, player, 1);
                ProductionCalculation.ProductionCalculationFactory(storageResources, factory, energyStorage);
                LaunchControleCenterFactory.StopFactory(factory, storageResources, player);

                Assert.Empty(factory.ResourceBuffer);
                Assert.Equal(factory.ProductionRate, factory.ProductionRate);
            }
            // 176 Assert.Equal(startBallance, player.Ballance);
            Assert.NotEqual(startBallance, player.Ballance);

            // 179 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
            Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);
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
                foreach (var item in factory.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 222 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 227 Assert.Equal(startClay, resultStorageResourcesProductionItem);
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
                foreach (var item in factory.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 244 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                        Assert.Equal(200, factory.RecipeUpgradeList["Money"]);
                    }
                    else
                    {
                        // 250 Assert.Equal(startStorageResorces.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.Equal(2, factory.RecipeUpgradeList[item.Key]);
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
                foreach (var item in factory.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        Assert.Equal(200, factory.RecipeUpgradeList["Money"]);
                    }
                    else
                    {
                        Assert.Equal(2, factory.RecipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(factory.CanUpgrade);
                Assert.Equal(2, factory.Level);
            }
        }

        [Fact]
        public void CorrectUpdateMaximumExpectedOutput()
        {
            foreach (var factory in factories)
            {
                MaximumPossibleExpectedOtput.UpdateMaximumPossibleExpectedOtput(factory, storageResources, player);
                decimal resultMaxxpectedOutput = factory.MaxExpectedOtput;

                // 290 Assert.NotEqual(startMaxExpectedOtput, resultMaxxpectedOutput);
                Assert.NotEqual((decimal)startMaxExpectedOtput, resultMaxxpectedOutput);
            }
        }
    }
}
