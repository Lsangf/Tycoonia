using Tycoonia.Application.Storage.Energy;
using Tycoonia.Application.Storage.Resources;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class EnergyStorageOperations
    {
        private StorageResources storageResources;
        private Dictionary<string, StorageResourcesBase> startStorageResources;
        private EnergyStorage energyStorage;
        private long startBallance;
        private PlayerReal player;

        private long startClay;
        private long startCoal;
        private long startUranium;
        private long startBricks;

        public EnergyStorageOperations()
        {
            storageResources = new StorageResources();

            startClay = storageResources.StorageList["Clay"].CurrentQuantity;
            startCoal = storageResources.StorageList["Coal"].CurrentQuantity;
            startUranium = storageResources.StorageList["Uranium"].CurrentQuantity;
            startBricks = storageResources.StorageList["Bricks"].CurrentQuantity;

            startStorageResources = new Dictionary<string, StorageResourcesBase>(storageResources.StorageList);
            energyStorage = new EnergyStorage();
            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        [Fact]
        public void CorrectCanUpgradeStorage()
        {
            EnergyStorageUpgrade.CanUpgradeStorage(energyStorage, storageResources, player);

            Assert.True(energyStorage.CanUpgrade);
        }

        [Fact]
        public void CorrectUpgradeSubtractionStorage()
        {
            EnergyStorageUpgrade.UpgradeSubtractionStorage(energyStorage, storageResources, player);
            long resultStorageResourceBricks = storageResources.StorageList["Bricks"].CurrentQuantity;
            long resultPlayerBallance = player.Ballance;

            Assert.NotEqual(startBricks, resultStorageResourceBricks);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }

        [Fact]
        public void CorrectUpgradeStorageResources()
        {
            EnergyStorageUpgrade.UpgradeStorage(energyStorage, storageResources, player);
            long resultStorageResourceBricks = storageResources.StorageList["Bricks"].CurrentQuantity;
            long resultPlayerBallance = player.Ballance;

            Assert.NotEqual(startBricks, resultStorageResourceBricks);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }
    }
}
