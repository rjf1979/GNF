namespace GNF.Domain.UnitOfWork
{
    public abstract class DbContext :IDbContext
    {
        public abstract TDbContext GetDbContext<TDbContext>(IConnectionStringResolver connectionStringResolver);
    }
}
