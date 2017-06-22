using System.Data.Entity;

namespace GNF.EFUnitwork.UnitOfWork
{
    public class DbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        /// <summary>
        /// </summary>
        public DbContextProvider(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TDbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
