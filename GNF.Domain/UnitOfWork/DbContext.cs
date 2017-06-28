namespace GNF.Domain.UnitOfWork
{
    public abstract class DbContext<TDbContext> :IDbContext<TDbContext>
    {
        private readonly IConnectionStringResolver _connectionStringResolver;

        protected DbContext(IConnectionStringResolver connectionStringResolver)
        {
            _connectionStringResolver = connectionStringResolver;
        }

        public abstract TDbContext DbContext { get; }
    }
}
