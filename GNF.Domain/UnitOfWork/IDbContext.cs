namespace GNF.Domain.UnitOfWork
{
    public interface IDbContext
    {
        TDbContext GetDbContext<TDbContext>(IConnectionStringResolver connectionStringResolver);
    }
}
