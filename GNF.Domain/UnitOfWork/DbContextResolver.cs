using System;

namespace GNF.Domain.UnitOfWork
{
    public class DbContextResolver : IDbContextResolver
    {
        public virtual TDbContext Resolve<TDbContext>(IConnectionStringResolver connectionStringResolver)
        {
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), connectionStringResolver.GetNameOrConnectionString());
        }
    }
}
