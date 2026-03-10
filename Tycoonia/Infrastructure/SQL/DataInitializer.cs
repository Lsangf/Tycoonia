using Tycoonia.Application.Interfaces;
using Tycoonia.Domain.Buildings.Factory;

namespace Tycoonia.Infrastructure.SQL
{
    public class DataInitializer
    {
        private readonly IRepository<FactoryBase> _factoryRepository;

        public DataInitializer(IRepository<FactoryBase> factoryRepository)
        {
            _factoryRepository = factoryRepository;
        }

        public async Task Initialize()
        {
            if (await _factoryRepository.AnyAsync())
                return;

            await _factoryRepository.AddAsync(new FactoryAluminum());
            await _factoryRepository.AddAsync(new FactoryBatteries());
            await _factoryRepository.AddAsync(new FactoryBricks());
            await _factoryRepository.AddAsync(new FactoryConcrete());
            await _factoryRepository.AddAsync(new FactoryCopperWire());
            await _factoryRepository.AddAsync(new FactoryDiamonds());
            await _factoryRepository.AddAsync(new FactoryElectronicComponents());
            await _factoryRepository.AddAsync(new FactoryEnergyStorage());
            await _factoryRepository.AddAsync(new FactoryEnrichmentUranium());
            await _factoryRepository.AddAsync(new FactoryFuel());
            await _factoryRepository.AddAsync(new FactoryGlass());
            await _factoryRepository.AddAsync(new FactoryGoldBars());
            await _factoryRepository.AddAsync(new FactoryPlastic());
            await _factoryRepository.AddAsync(new FactoryPurifiedLithium());
            await _factoryRepository.AddAsync(new FactorySilicon());
            await _factoryRepository.AddAsync(new FactorySilverBars());
            await _factoryRepository.AddAsync(new FactorySolidFuel());
            await _factoryRepository.AddAsync(new FactorySteel());
            await _factoryRepository.AddAsync(new FactoryThoriumRod());
            await _factoryRepository.AddAsync(new FactoryTitanium());
            await _factoryRepository.AddAsync(new FactoryUraniumRod());
        }
    }
}
