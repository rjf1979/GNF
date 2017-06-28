using GNF.Domain.UnitOfWork;
using SqlSugar;

namespace GNF.SqlSugarUnitOfWork
{
    public class SqlDbContext : IDbContext<SqlSugarClient>
    {
        public SqlDbContext(IConnectionStringResolver connectionStringResolver)
        {
            Context = new SqlSugarClient(new ConnectionConfig{ ConnectionString = connectionStringResolver.GetConnectionString(),DbType = DbType.SqlServer, InitKeyType = InitKeyType.Attribute,IsAutoCloseConnection = false });
        }

        public SqlSugarClient Context { get; }
    }
}
