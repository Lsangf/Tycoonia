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
        private int startProductionTimeEnergyPlant;
        private EnergyStorage startEnergyStorage;
        private long startBallance;
        private PlayerReal player;
        private long startMaxExpectedOtput;

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
            startProductionTimeEnergyPlant = 10;
            startEnergyStorage = new EnergyStorage();

            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);

            startMaxExpectedOtput = 0;
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void CorrectPreparationBufferAndSubtractionStorage()
        {
            foreach (var energyPlant in energyPlants)
            {
                LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(energyPlant, storageResources, energyStorage, player, 1);

                // 60 Assert.Null(energyPlant.ResourceBuffer);
                Assert.NotNull(energyPlant.ResourceBuffer);

                // 65 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
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
                energyPlant.ProductionTime = startProductionTimeEnergyPlant;
                LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(energyPlant, storageResources, energyStorage, player, 1);
                Dictionary<string, int> resultRateDictionary = EnergeticsCalculation.EnergeticsCalculationEnergyPlant(storageResources, energyPlant, energyStorage, 0);///!!!!!!!!!!!!
                decimal resultProductionTimeEnergyPlant = energyPlant.ProductionTime;

                // 91 Assert.Null(energyPlant.ResourceBuffer);
                Assert.NotNull(energyPlant.ResourceBuffer);

                Assert.Equal(energyPlant.ProductionItemList, resultRateDictionary);

                // 96 Assert.Equal(startProductionTimeEnergyPlant, resultProductionTimeEnergyPlant);
                Assert.NotEqual(startProductionTimeEnergyPlant, resultProductionTimeEnergyPlant);
            }
            // 99 Assert.Equal(startStorageResources.StorageList, storageResources.StorageList);
            Assert.NotEqual(startStorageResources.StorageList, storageResources.StorageList);

            // 102 Assert.Equal(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
            Assert.NotEqual(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
        }

        [Fact]
        public void CorrectSubtractionTime()
        {
            foreach (var energyPlant in energyPlants)
            {
                energyPlant.ProductionTime = startProductionTimeEnergyPlant;
                energyPlant.ProductionTimePerIteration = 10m;
                TimeSubtractionBuilding.TimeSubtraction(energyPlant);
                decimal resultProductionTimeEnergyPlant = energyPlant.ProductionTime;

                // 116 Assert.Equal(startProductionTimeEnergyPlant, resultProductionTimeEnergyPlant);
                Assert.NotEqual(startProductionTimeEnergyPlant, resultProductionTimeEnergyPlant);
                Assert.Equal(0, resultProductionTimeEnergyPlant);
            }
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
                foreach (var item in energyPlant.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 145 Assert.Equal(startBallance, player.Ballance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 147 Assert.Equal(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
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
                foreach (var item in energyPlant.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 164 Assert.Equal(startBallance, player.Ballance);
                        Assert.NotEqual(startBallance, player.Ballance);
                        Assert.Equal(200, energyPlant.RecipeUpgradeList["Money"]);
                    }
                    else
                    {
                        // 170 Assert.Equal(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.Equal(2, energyPlant.RecipeUpgradeList[item.Key]);
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
                foreach (var item in energyPlant.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        Assert.Equal(200, energyPlant.RecipeUpgradeList["Money"]);
                    }
                    else
                    {
                        Assert.Equal(2, energyPlant.RecipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(energyPlant.CanUpgrade);
                Assert.Equal(2, energyPlant.Level);
            }
        }

        [Fact]
        public void CorrectUpdateMaximumExpectedOutput()
        {
            foreach (var energyPlant in energyPlants)
            {
                MaximumPossibleExpectedOtput.UpdateMaximumPossibleExpectedOtput(energyPlant, storageResources, player);
                long resultMaxxpectedOutput = energyPlant.MaxExpectedOtput;

                // 210 Assert.NotEqual(startMaxExpectedOtput, resultMaxxpectedOutput);
                Assert.NotEqual(startMaxExpectedOtput, resultMaxxpectedOutput);
            }
        }
    }
}
