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
        private EnergyStorage startEnergyStorage;
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
            startEnergyStorage = new EnergyStorage();

            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void CorrectPreparationBufferAndSubtractionStorage()
        {
            foreach (var energyPlant in energyPlants)
            {
                LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(energyPlant, storageResources, energyStorage, player, 1);

                // 57 Assert.Null(energyPlant.ResourceBuffer);
                Assert.NotNull(energyPlant.ResourceBuffer);

                // 60 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
                Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);
                Assert.True(energyPlant.WorkFlag);
            }
        }

        [Fact]
        public void CorrectPreparationLaunch()
        {
            foreach (var energyPlant in energyPlants)
            {
                LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(energyPlant, storageResources, energyStorage, player, 1);
                Assert.True(energyPlant.WorkFlag);
            }
        }

        [Fact]
        public void CorrectProductYield()
        {
            foreach (var energyPlant in energyPlants)
            {
                LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(energyPlant, storageResources, energyStorage, player, 1);
                Dictionary<string, int> resultRateDictionary = EnergeticsCalculation.EnergeticsCalculationEnergyPlant(storageResources, energyPlant, energyStorage);

                // 84 Assert.Null(energyPlant.ResourceBuffer);
                Assert.NotNull(energyPlant.ResourceBuffer);

                Assert.Equal(energyPlant.ProductionItemList, resultRateDictionary);
            }
            // 89 Assert.Equal(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
            Assert.NotEqual(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
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
                UpgradeBuilding.UpgradeSubtraction(energyPlant, storageResources, player);
                foreach (var item in energyPlant.ReceipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 113 Assert.Equal(startBallance, player.Ballance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 118 Assert.Equal(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
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
                        // 135 Assert.Equal(startBallance, player.Ballance);
                        Assert.NotEqual(startBallance, player.Ballance);
                        Assert.Equal(200, energyPlant.ReceipeUpgradeList["Money"]);
                    }
                    else
                    {
                        // 141 Assert.Equal(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
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
