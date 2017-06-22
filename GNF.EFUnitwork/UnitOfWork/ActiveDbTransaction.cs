using System.Data.Entity;

namespace GNF.EFUnitwork.UnitOfWork
{
    public class ActiveDbTransaction
    {
        public DbContextTransaction DbContextTransaction { get; }

        public DbContext StarterDbContext { get; }

        public ActiveDbTransaction(DbContextTransaction dbContextTransaction, DbContext dbContext)
        {
            DbContextTransaction = dbContextTransaction;
            StarterDbContext = dbContext;
        }
    }
}
