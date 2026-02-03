using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI;

namespace Tycoonia.Core
{
    public class GameLoop
    {
        public static void StartGameLoop(PlayerReal player, List<FactoryBase> factories, List<EnergyPlantBase> energyPlants, StorageResources storageResources, EnergyStorage energyStorage)
        {
            // ConsoleStartSystem.
            ConsoleChoiceSystem.ConsoleChoice(player, null, factories, energyPlants, storageResources, energyStorage);


        }
    }
}
