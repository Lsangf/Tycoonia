using Tycoonia.Application.Interfaces;
using Tycoonia.Domain.Buildings.Factory;

namespace Tycoonia.Application.Services
{
    public class FactoryService
    {
        private readonly IFactoryRepository _factoryRepository;

        public FactoryService(IFactoryRepository factoryRepository)
        {
            _factoryRepository = factoryRepository;
        }

        public async Task UpgradeFactory(int id)
        {
            await _factoryRepository.UpgradeFactoryAsync(id);
        }

        public async Task<IEnumerable<FactoryBase>> GetAllFactoriesAsync()
        {
            return await _factoryRepository.GetAllAsync();
        }
    }
}
