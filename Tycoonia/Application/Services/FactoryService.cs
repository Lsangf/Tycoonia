using Tycoonia.Application.Interfaces;
using Tycoonia.Domain.Buildings.Factory;

namespace Tycoonia.Application.Services
{
    public class FactoryService
    {
        private readonly IRepository<FactoryBase> _factoryRepository;

        public FactoryService(IRepository<FactoryBase> factoryRepository)
        {
            _factoryRepository = factoryRepository;
        }

        public async Task<FactoryBase> GetByIdFactory(int id)
        {
            return await _factoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateFactory(FactoryBase factory)
        {
            await _factoryRepository.UpdateAsync(factory);
        }

        public async Task<IEnumerable<FactoryBase>> GetAllFactoriesAsync()
        {
            return await _factoryRepository.GetAllAsync();
        }
    }
}
