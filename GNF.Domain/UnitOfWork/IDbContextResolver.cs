namespace GNF.Domain.UnitOfWork
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>(IConnectionStringResolver connectionStringResolver);
    }
}
