using System.Data.Entity;

namespace GNF.EFUnitwork
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
