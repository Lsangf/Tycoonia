using Tycoonia.Application.Energy;
using Tycoonia.Application.Storage;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.EnergyPlant.TPP;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class EnergyPlantOperations
    {
        private StorageResources storageResources;
        private Dictionary<string, StorageResourcesBase> startStorageResources;
        private EnergyPlantBase coalTPP;
        private List<EnergyPlantBase> energyPlants;
        private EnergyStorage energyStorage;
        private long startBallance;
        private PlayerReal player;

        // private long startClay;
        private long startCoal;
        private long startUranium;
        private long startBricks;

        public EnergyPlantOperations()
        {
            storageResources = new StorageResources();

            // startClay = storageResources.StorageList["Clay"].CurrentQuantity;
            startCoal = storageResources.StorageList["Coal"].CurrentQuantity;
            startUranium = storageResources.StorageList["Uranium"].CurrentQuantity;
            startBricks = storageResources.StorageList["Bricks"].CurrentQuantity;

            startStorageResources = new Dictionary<string, StorageResourcesBase>(storageResources.StorageList);
            coalTPP = new CoalTPP();
            energyPlants = [coalTPP];
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
            Assert.NotEqual(startCoal, resultStorageResorces["Coal"].CurrentQuantity);

            // 59 Assert.NotEqual(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);
            Assert.Equal(startUranium, resultStorageResorces["Uranium"].CurrentQuantity);

            //// 62 Assert.NotEqual(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);
            //Assert.Equal(startBricks, resultStorageResorces["Bricks"].CurrentQuantity);

        }

        [Fact]
        public void CorrectPreparationLaunch()
        {
            LaunchControleCenterEnergyPlant.PreparationLaunchEnergyPlant(coalTPP, storageResources, energyStorage, player);
            bool resultEnergyPlantWorkFlag = coalTPP.WorkFlag;

            Assert.True(resultEnergyPlantWorkFlag);
        }

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
    }
}
