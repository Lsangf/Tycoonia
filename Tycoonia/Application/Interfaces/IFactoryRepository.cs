using Tycoonia.Domain.Buildings.Factory;

namespace Tycoonia.Application.Interfaces
{
    public interface IFactoryRepository : IRepository<FactoryBase>
    {
        Task UpgradeFactoryAsync(int factoryId);
    }
}
