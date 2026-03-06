using Tycoonia.Domain;
using Tycoonia.Infrastructure.SQL.Database;

namespace Tycoonia.Infrastructure.SQL.Repositories
{
    public abstract class Repository<T> where T : EntityBase
    {
        protected readonly DbConnectionProvider _connectionProvider;

        protected Repository(DbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
    }
}
