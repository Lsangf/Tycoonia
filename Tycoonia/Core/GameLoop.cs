using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI;

namespace Tycoonia.Core
{
    public class GameLoop
    {
        public static void StartGameLoop(List<MineBase> mines, PlayerReal player, List<FactoryBase> factories, List<EnergyPlantBase> energyPlants, StorageResources storageResources, EnergyStorage energyStorage)
        {
            // ConsoleStartSystem.
            ConsoleChoiceSystem.ConsoleChoice(mines, player, factories, energyPlants, storageResources, energyStorage);
        }
    }
}
