using Tycoonia.Application.Mining;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Resources.Storage;
namespace TycooniaTest
{
    public class MineOperations
    {
        [Fact]
        public void MineOperationsExtraction()
        {
            StorageResources storageResources = new();
            MineClay clayMine = new((0, 0));
            MineCoal coalMine = new((0, 0));
            List<MineBase> mines = [clayMine, coalMine];
            EnergyStorage energyStorage = new();


            ResourceExtraction.ResourceExtractionMine(storageResources, mines, energyStorage);
            long resultMiningClay = storageResources.StorageList[mines[0].ProductionItem].CurrentQuantity;
            long resultMiningCoal = storageResources.StorageList[mines[1].ProductionItem].CurrentQuantity;
            decimal resultEnergyStorage = energyStorage.CurrentStorage;

            Assert.Equal(15, resultMiningClay);
            Assert.Equal(11, resultMiningCoal);
            Assert.Equal(49999.8m, resultEnergyStorage);
        }
    }
}