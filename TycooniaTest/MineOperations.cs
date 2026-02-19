using Tycoonia.Application.Mining;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
namespace TycooniaTest
{
    public class MineOperations
    {
        private StorageResources storageResources;
        private MineBase mineBauxite;
        private MineBase mineClay;
        private MineBase mineCoal;
        private MineBase mineCopper;
        private MineBase mineGold;
        private MineBase mineGravel;
        private MineBase mineIron;
        private MineBase mineLimenite;
        private MineBase mineLimestone;
        private MineBase mineLithium;
        private MineBase mineOil;
        private MineBase mineQuartzSand;
        private MineBase mineRoughDiamonds;
        private MineBase mineSilver;
        private MineBase mineThorium232;
        private MineBase mineUranium;
        private MineBase mineWater;

        private StorageResources startStorageResources;
        private List<MineBase> mines;
        private EnergyStorage energyStorage;
        private EnergyStorage startEnergyStorage;
        private PlayerReal player;
        private int startBallance;

        public MineOperations()
        {
            {
                startStorageResources = new StorageResources();
                storageResources = new StorageResources();

                mineBauxite = new MineBauxite((0, 0));
                mineClay = new MineClay((0, 0));
                mineCoal = new MineCoal((0, 0));
                mineCopper = new MineCopper((0, 0));
                mineGold = new MineGold((0, 0));
                mineGravel = new MineGravel((0, 0));
                mineIron = new MineIron((0, 0));
                mineLimenite = new MineLimenite((0, 0));
                mineLimestone = new MineLimestone((0, 0));
                mineLithium = new MineLithium((0, 0));
                mineOil = new MineOil((0, 0));
                mineQuartzSand = new MineQuartzSand((0, 0));
                mineRoughDiamonds = new MineRoughDiamonds((0, 0));
                mineSilver = new MineSilver((0, 0));
                mineThorium232 = new MineThorium232((0, 0));
                mineUranium = new MineUranium((0, 0));
                mineWater = new MineWater((0, 0));

                mines =
                    [
                    mineBauxite, mineClay, mineCoal, mineCopper, mineGold, mineGravel,
                    mineIron, mineLimenite, mineLimestone, mineLithium, mineOil,
                    mineQuartzSand, mineRoughDiamonds, mineSilver, mineThorium232,
                    mineUranium, mineWater
                    ];

                energyStorage = new EnergyStorage();
                startEnergyStorage = new EnergyStorage();

                startBallance = 10000;
                player = new PlayerReal("player1", startBallance);
            }
        }

        // "//(line number)" to check exact values ​​without comparing correctness

        [Fact]
        public void CorrectEnergyReservation()
        {
            foreach (var mine in mines)
            {
                ResourceExtraction.EnergyReservation(energyStorage, mine);
            }
            // 85 Assert.NotEqual(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
            Assert.NotEqual(startEnergyStorage.CurrentStorage, energyStorage.CurrentStorage);
        }

        [Fact]
        public void CorrectSaveExtraction()
        {
            foreach (var mine in mines)
            {
                ResourceExtraction.SaveExtractionMine(storageResources, mine);

                // 96 Assert.Equal(startStorageResources.StorageList[mine.ProductionItem].CurrentQuantity, storageResources.StorageList[mine.ProductionItem].CurrentQuantity);
                Assert.NotEqual(startStorageResources.StorageList[mine.ProductionItem].CurrentQuantity, storageResources.StorageList[mine.ProductionItem].CurrentQuantity);
            }
        }

        [Fact]
        public void CorrectResourceExtraction()
        {
            foreach (var mine in mines)
            {
                ResourceExtraction.ResourceExtractionMine(storageResources, mine, energyStorage);

                // 108 Assert.Equal(startStorageResources.StorageList[mine.ProductionItem].CurrentQuantity, storageResources.StorageList[mine.ProductionItem].CurrentQuantity);
                Assert.NotEqual(startStorageResources.StorageList[mine.ProductionItem].CurrentQuantity, storageResources.StorageList[mine.ProductionItem].CurrentQuantity);
            }
            // 111 Assert.Equal(startEnergyStorage, energyStorage);
            Assert.NotEqual(startEnergyStorage, energyStorage);
        }

        [Fact]
        public void CorrectCanUpgrade()
        {
            foreach (var mine in mines)
            {
                MineUpgrade.CanUpgrade(mine, storageResources, player);
                Assert.True(mine.CanUpgrade);
            }
        }

        [Fact]
        public void CorrectUpgradeSubtraction()
        {
            foreach (var mine in mines)
            {
                MineUpgrade.UpgradeSubtraction(mine, storageResources, player);
                foreach (var item in mine.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 135 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                    }
                    else
                    {
                        // 140 Assert.Equal(startClay, resultStorageResourcesProductionItem);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                    }
                }
            }
        }

        [Fact]
        public void CorrectUpgradeStorage()
        {
            foreach (var mine in mines)
            {
                MineUpgrade.Upgrade(mine, storageResources, player);
                foreach (var item in mine.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        // 157 Assert.Equal(startBallance, resultPlayerBallance);
                        Assert.NotEqual(startBallance, player.Ballance);
                        Assert.Equal(200, mine.RecipeUpgradeList["Money"]);
                    }
                    else
                    {
                        // 163 Assert.Equal(startStorageResorces.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.NotEqual(startStorageResources.StorageList[item.Key].CurrentQuantity, storageResources.StorageList[item.Key].CurrentQuantity);
                        Assert.Equal(2, mine.RecipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(mine.CanUpgrade);
                Assert.Equal(2, mine.Level);
            }
        }

        [Fact]
        public void CorrectUpdateUpgradeAmount()
        {
            foreach (var mine in mines)
            {
                MineUpgrade.UpdateUpgradeAmount(mine);
                foreach (var item in mine.RecipeUpgradeList)
                {
                    if (item.Key == "Money")
                    {
                        Assert.Equal(200, mine.RecipeUpgradeList["Money"]);
                    }
                    else
                    {
                        Assert.Equal(2, mine.RecipeUpgradeList[item.Key]);
                    }
                }
                Assert.False(mine.CanUpgrade);
                Assert.Equal(2, mine.Level);
            }
        }
    }
}