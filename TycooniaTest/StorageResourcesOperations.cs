using Tycoonia.Application.Storage.Resources;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;

namespace TycooniaTest
{
    public class StorageResourcesOperations
    {
        private StorageResources storageResources;
        private long startBallance;
        private PlayerReal player;

        public StorageResourcesOperations()
        {
            storageResources = new StorageResources();

            startBallance = 10000;
            player = new PlayerReal("player1", startBallance);
        }

        [Fact]
        public void CorrectCanUpgradeStorage()
        {
            foreach (var resource in storageResources.StorageList)
            {
                StorageResourcesBase upgradeResourceList = storageResources.StorageList[resource.Key];
                StorageResourcesUpgrade.CanUpgradeStorage(player, upgradeResourceList);

                Assert.True(upgradeResourceList.CanUpgrade);
            }
        }

        [Fact]
        public void CorrectUpgradeSubtractionStorage()
        {
            foreach (var resource in storageResources.StorageList)
            {
                StorageResourcesBase upgradeResourceList = storageResources.StorageList[resource.Key];
                StorageResourcesUpgrade.UpgradeSubtractionStorage(player, upgradeResourceList);

                Assert.NotEqual(startBallance, player.Ballance);
            }
        }

        [Fact]
        public void CorrectUpdateUpgradeAmount()
        {
            foreach (var resource in storageResources.StorageList)
            {
                StorageResourcesBase upgradeResourceList = storageResources.StorageList[resource.Key];
                StorageResourcesUpgrade.UpdateUpgradeAmount(upgradeResourceList);

                Assert.False(upgradeResourceList.CanUpgrade);
                Assert.Equal(20, upgradeResourceList.UpgradeCost);
                Assert.Equal(2, upgradeResourceList.Level);
            }
        }

        [Fact]
        public void CorrectUpgradeStorageResources()
        {
            foreach (var resource in storageResources.StorageList)
            {
                StorageResourcesUpgrade.UpgradeStorage(resource.Key, storageResources, player);
                StorageResourcesBase upgradeResourceList = storageResources.StorageList[resource.Key];

                Assert.False(storageResources.StorageList[resource.Key].CanUpgrade);
                Assert.Equal(2, storageResources.StorageList[resource.Key].Level);
                Assert.Equal(20, storageResources.StorageList[resource.Key].UpgradeCost);
                Assert.NotEqual(startBallance, player.Ballance);
            }
        }
    }
}
