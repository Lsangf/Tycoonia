using Tycoonia.Application.Mining;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Resources.RawResources;
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
            long resultMiningClay = storageResources.Storage[mines[0].Name];
            decimal resultEnergyStorage = energyStorage.CurrentStorage;

            Assert.Equal(5, resultMiningClay);
            Assert.Equal(49999.8m, resultEnergyStorage);
        }
    }
}