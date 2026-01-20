using Tycoonia.Application.Factory;
using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.EnergyResources;
using Tycoonia.Domain.Resources.ProcessedResources;
using Tycoonia.Domain.Resources.RawResources;
using Tycoonia.Domain.Resources.Storage;
using Tycoonia.Presentation.UI;

namespace Tycoonia.Core
{
    public class GameLoop
    {
        public static void StartGameLoop(
            PlayerReal player,
            Clay clay, Coal coal, Bricks bricks, Energy energy,
            List<FactoryBase> factories, List<EnergyPlantBase> energyPlants,
            StorageResources storageResources, EnergyStorage energyStorage)
        {

            ConsoleChoiceSystem.ConsoleChoice(null, factories, energyPlants, storageResources, energyStorage);

            //byte choice = ConsoleChoiceSystem.ConsoleChoice();
            //switch (choice)
            //{
            //    case 1
            //}

            //int magicNumber = 1;
            //switch (magicNumber)
            //{
            //    case 1:
            //        // factories
            //        break;
            //}
            //LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);

            //foreach (var factory in factories)
            //{
            //    if (!factory.WorkFlag)
            //    {
            //        continue;
            //    }

            //}

            //LaunchControleCenterFactory.PreparationLaunchFactory(factoryBricks, storageResources, energyStorage, player);


        }
    }
}
