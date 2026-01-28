using Tycoonia.Application;
using Tycoonia.Application.Energy;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.NPP;
using Tycoonia.Domain.Buildings.EnergyPlant.TPP;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class EnergyPlantOperations
    {
        private StorageResources storageResources;
        private StorageResources startStorageResources;

        private EnergyPlantBase coalTPP;
        private EnergyPlantBase fuelTPP;
        private EnergyPlantBase solidFuelTPP;
        private EnergyPlantBase uraniumNPP;
        private EnergyPlantBase thoriumNPP;

        private List<EnergyPlantBase> energyPlants;
        private EnergyStorage energyStorage;
        private long startBallance;
        private PlayerReal player;

        public EnergyPlantOperations()
        {
            storageResources = new StorageResources();
            startStorageResources = new StorageResources();

            coalTPP = new CoalTPP();
            fuelTPP = new FuelTPP();
            solidFuelTPP = new SolidFuelTPP();
            uraniumNPP = new UraniumNPP();
            thoriumNPP = new ThoriumNPP();

            energyPlants = [coalTPP, fuelTPP, solidFuelTPP, uraniumNPP, thoriumNPP];

            energyStorage = new EnergyStorage();
            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void CorrectPreparationBufferAndSubtractionStorage()
        {
            LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(coalTPP, storageResources, energyStorage, player);
            Dictionary<string, StorageResourcesBase> resultStorageResorces = storageResources.StorageList;
            long resultPlayerBallance = player.Ballance;
            Dictionary<string, StorageResourcesBase> resultEnergyPlantResourceBuffer = coalTPP.ResourceBuffer;

            // 53 Assert.Null(resultEnergyPlantResourceBuffer);
            Assert.NotNull(resultEnergyPlantResourceBuffer);

            // 56 Assert.Equal(startCoal, resultStorageResorces["Coal"].CurrentQuantity);
            Assert.NotEqual(startStorageResources.StorageList["Coal"].CurrentQuantity, resultStorageResorces["Coal"].CurrentQuantity);

            // 59 Assert.NotEqual(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);
            Assert.Equal(startStorageResources.StorageList["Uranium"].CurrentQuantity, resultStorageResorces["Uranium"].CurrentQuantity);

            // 62 Assert.NotEqual(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);
            Assert.Equal(startStorageResources.StorageList["Bricks"].CurrentQuantity, resultStorageResorces["Bricks"].CurrentQuantity);

        }

        //[Fact] // When all resources are added to the storage, enable the test
        //public void CorrectPreparationLaunch()
        //{
        //    foreach (var energyPlant in energyPlants)
        //    {
        //        LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(energyPlant, storageResources, energyStorage, player);
        //        bool resultEnergyPlantWorkFlag = energyPlant.WorkFlag;
        //        Assert.True(resultEnergyPlantWorkFlag); 
        //    }
        //}

        [Fact]
        public void CorrectProductYield()
        {
            LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(coalTPP, storageResources, energyStorage, player);
            Dictionary<string, int> resultRateDictionary = EnergeticsCalculation.EnergeticsCalculationEnergyPlant(storageResources, coalTPP, energyStorage);
            string productionItemKey = energyPlants[0].ProductionItemList.Keys.First();
            decimal resultEnergyStorage = energyStorage.CurrentStorage;

            //84 Assert.Null(coalTPP.ResourceBuffer);
            Assert.NotNull(coalTPP.ResourceBuffer);

            Assert.Equal(coalTPP.ProductionItemList, resultRateDictionary);

            Assert.Equal(50000m, resultEnergyStorage);
        }

        [Fact]
        public void CorrectCanUpgrade()
        {
            foreach (var energyPlant in energyPlants)
            {
                UpgradeBuilding.CanUpgrade(energyPlant, storageResources, player);
                Assert.True(energyPlant.CanUpgrade);
            }
        }

        [Fact]
        public void CorrectUpgradeSubtraction()
        {
            foreach (var energyPlant in energyPlants)
            {
                UpgradeBuilding.UpgradeSubtraction(coalTPP, storageResources, player);
                foreach (var item in energyPlant.ReceipeUpgradeList)
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
            foreach (var energyPlant in energyPlants)
            {
                UpgradeBuilding.Upgrade(energyPlant, storageResources, player);
                foreach (var item in energyPlant.ReceipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 151 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                        Assert.Equal(200, energyPlant.ReceipeUpgradeList["Money"]);
                    }
                    else
                    {
                        // 148 Assert.Equal(startStorageResorces.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.Equal(2, energyPlant.ReceipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(energyPlant.CanUpgrade);
                Assert.Equal(2, energyPlant.Level);
            }
        }

        [Fact]
        public void CorrectUpdateUpgradeAmount()
        {
            foreach (var energyPlant in energyPlants)
            {
                UpgradeBuilding.UpdateUpgradeAmount(energyPlant);
                foreach (var item in energyPlant.ReceipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        Assert.Equal(200, energyPlant.ReceipeUpgradeList["Money"]);
                    }
                    else
                    {
                        Assert.Equal(2, energyPlant.ReceipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(energyPlant.CanUpgrade);
                Assert.Equal(2, energyPlant.Level);
            }
        }
    }
}
