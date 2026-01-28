using Tycoonia.Application.Storage.Energy;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class EnergyStorageOperations
    {
        private StorageResources storageResources;
        private EnergyStorage energyStorage;
        private long startBallance;
        private PlayerReal player;
        private long startBricks;

        public EnergyStorageOperations()
        {
            storageResources = new StorageResources();

            startBricks = storageResources.StorageList["Bricks"].CurrentQuantity;

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
        public void CorrectUpdateUpgradeAmount()
        {
            EnergyStorageUpgrade.UpdateUpgradeAmount(energyStorage);

            Assert.False(energyStorage.CanUpgrade);
            Assert.Equal(20, energyStorage.ReceipeUpgradeList["Money"]);
            Assert.Equal(2, energyStorage.ReceipeUpgradeList["Bricks"]);
            Assert.Equal(2, energyStorage.Level);
        }

        [Fact]
        public void CorrectUpgradeStorage()
        {
            EnergyStorageUpgrade.UpgradeStorage(energyStorage, storageResources, player);
            long resultStorageResourceBricks = storageResources.StorageList["Bricks"].CurrentQuantity;
            long resultPlayerBallance = player.Ballance;

            Assert.False(energyStorage.CanUpgrade);
            Assert.Equal(2, energyStorage.Level);
            Assert.NotEqual(startBricks, resultStorageResourceBricks);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }
    }
}
