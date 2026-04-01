using Tycoonia.Application.Factory;
using Tycoonia.Application.Services;
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
        private readonly FactoryService _factoryService;
        private PlayerReal _player;
        private StorageResources _storageResources;
        private Domain.Resources.Storage.EnergyStorage _energyStorage;
        private List<FactoryBase> _factories;
        private List<MineBase> _mines;
        private List<EnergyPlantBase> _energyPlants;

        public GameLoop(List<FactoryBase> factories, FactoryService factoryService, PlayerReal player, StorageResources storageResources, Domain.Resources.Storage.EnergyStorage energyStorage, List<MineBase> mines, List<EnergyPlantBase> energyPlants)
        {
            _factoryService = factoryService;
            _player = player;
            _storageResources = storageResources;
            _energyStorage = energyStorage;
            _factories = factories;
            _mines = mines;
            _energyPlants = energyPlants;
        }

        public async Task StartAsync()
        {
            DateTime currentTime = DateTime.UtcNow;
            decimal differenceSeconds = 0m;

            foreach (FactoryBase factory in _factories)
            {
                if (!factory.WorkFlag)
                    continue;

                differenceSeconds = (decimal)(currentTime - factory.LastUpdateTime).TotalSeconds;

                if (differenceSeconds < 0)
                    differenceSeconds = 0;

                decimal remainingOutput = factory.TargetOutput - factory.Produced;
                if (differenceSeconds >= remainingOutput / factory.ProductionRate)
                {
                    await SaveOfflineInStorage.SaveOffline(_factoryService, _storageResources, _energyStorage, factory);
                }
                else
                {
                    _ = FactorySystem.UpdateFactoryCalculations(_factoryService, _storageResources, factory, _energyStorage, _player);
                }
            }
            await ConsoleChoiceSystem.ConsoleChoiceAsync(_factories, _mines, _player, _factoryService, _energyPlants, _storageResources, _energyStorage);
        }
    }
}
