using Tycoonia.Application.Storage.Resources;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Xunit;

namespace TycooniaTest
{
    public class StorageResourcesOperations
    {
        private StorageResources storageResources;
        private Dictionary<string, StorageResourcesBase> startStorageResources;
        private long startBallance;
        private PlayerReal player;

        private bool startClayCanUpgrade;
        private bool startCoalCanUpgrade;
        private bool startUraniumCanUpgrade;
        private bool startBricksCanUpgrade;

        public StorageResourcesOperations()
        {
            storageResources = new StorageResources();

            startStorageResources = new Dictionary<string, StorageResourcesBase>(storageResources.StorageList);
            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        [Fact]
        public void CorrectCanUpgradeStorage()
        {
            StorageResourcesBase upgradeResourceList = storageResources.StorageList["Clay"];
            StorageResourcesUpgrade.CanUpgradeStorage(player, upgradeResourceList);

            StorageResourcesBase upgradeResourceList1 = storageResources.StorageList["Coal"];
            StorageResourcesUpgrade.CanUpgradeStorage(player, upgradeResourceList1);

            StorageResourcesBase upgradeResourceList2 = storageResources.StorageList["Uranium"];
            StorageResourcesUpgrade.CanUpgradeStorage(player, upgradeResourceList2);

            StorageResourcesBase upgradeResourceList3 = storageResources.StorageList["Bricks"];
            StorageResourcesUpgrade.CanUpgradeStorage(player, upgradeResourceList3);

            Dictionary<string, StorageResourcesBase> resultStorageResorces = storageResources.StorageList;

            Assert.True(resultStorageResorces["Clay"].CanUpgrade);
            Assert.True(resultStorageResorces["Coal"].CanUpgrade);
            Assert.True(resultStorageResorces["Uranium"].CanUpgrade);
            Assert.True(resultStorageResorces["Bricks"].CanUpgrade);
        }

        [Fact]
        public void CorrectUpgradeSubtractionStorage()
        {
            StorageResourcesBase upgradeResourceList = storageResources.StorageList["Clay"];
            StorageResourcesUpgrade.UpgradeSubtractionStorage(player, upgradeResourceList);

            StorageResourcesBase upgradeResourceList1 = storageResources.StorageList["Coal"];
            StorageResourcesUpgrade.UpgradeSubtractionStorage(player, upgradeResourceList1);

            StorageResourcesBase upgradeResourceList2 = storageResources.StorageList["Uranium"];
            StorageResourcesUpgrade.UpgradeSubtractionStorage(player, upgradeResourceList2);

            StorageResourcesBase upgradeResourceList3 = storageResources.StorageList["Bricks"];
            StorageResourcesUpgrade.UpgradeSubtractionStorage(player, upgradeResourceList3);

            long resultPlayerBallance = player.Ballance;

            Assert.NotEqual(startBallance, resultPlayerBallance);
        }


        [Fact]
        public void CorrectUpgradeStorageResources()
        {
            StorageResourcesUpgrade.UpgradeStorage(storageResources, player);
            Dictionary<string, StorageResourcesBase> resultStorageResorces = storageResources.StorageList;
            long resultPlayerBallance = player.Ballance;

            Assert.True(resultStorageResorces["Clay"].CanUpgrade);
            Assert.NotEqual(startBallance, resultPlayerBallance);
        }
    }
}
