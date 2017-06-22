using System.Data.Entity;

namespace GNF.EFUnitwork.UnitOfWork
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>(string connectionString)
            where TDbContext : DbContext;
    }
}
