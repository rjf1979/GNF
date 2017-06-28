using GNF.Domain.UnitOfWork;
using SqlSugar;

namespace GNF.SqlSugarUnitOfWork
{
    public class SqlDbContext: IDbContext<SqlSugarClient>
    {
        public SqlDbContext(IDbContextResolver dbContextResolver, IConnectionStringResolver connectionStringResolver)
        {
            Current = dbContextResolver.Resolve<SqlSugarClient>(connectionStringResolver);
        }

        public SqlDbContext(IConnectionStringResolver connectionStringResolver)
        {
            Current = new SqlSugarClient(new ConnectionConfig{ ConnectionString = connectionStringResolver.GetConnectionString(),DbType = DbType.SqlServer, InitKeyType = InitKeyType.Attribute,IsAutoCloseConnection = false });
        }

        public SqlSugarClient Current { get; }
        public void Dispose()
        {
            Current?.Dispose();
        }
    }
}
