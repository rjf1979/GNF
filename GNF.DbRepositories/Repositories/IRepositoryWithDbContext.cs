using System.Data.Entity;

namespace GNF.EFUnitwork.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
