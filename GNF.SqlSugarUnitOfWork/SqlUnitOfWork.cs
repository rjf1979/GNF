using System;
using System.Threading.Tasks;
using GNF.Domain.UnitOfWork;
using SqlSugar;

namespace GNF.SqlSugarUnitOfWork
{
    public class SqlUnitOfWork:UnitOfWorkBase<SqlSugarClient>
    {
        readonly SqlDbContext _dbContext;

        public SqlUnitOfWork(IConnectionStringResolver connectionStringResolver) : base(connectionStringResolver)
        {
            IDbContextResolver dbContextResolver = new DbContextResolver();
            _dbContext = dbContextResolver.Resolve<SqlDbContext>(connectionStringResolver);
            Transaction = new SqlTransaction(_dbContext);
        }

        public SqlDbContext GetDbContext()
        {
            return _dbContext;
        }

        public override IDbContext<SqlSugarClient> DbContext => _dbContext;
        public override ITransaction Transaction { get; }

        public override void Dispose()
        {
            _dbContext?.Current.Dispose();
        }
    }
}
