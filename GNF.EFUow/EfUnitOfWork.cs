using System;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using GNF.Domain.UnitOfWork;

namespace GNF.EFUow
{
    public class EfUnitOfWork : UnitOfWorkBase<DbContext>
    {
        readonly DbContext _dbContext;
        private readonly Stopwatch _stopwatch;

        public EfUnitOfWork(IConnectionStringResolver connectionStringResolver) : base(connectionStringResolver)
        {
            IDbContextResolver dbContextResolver = new DbContextResolver();
            _dbContext = dbContextResolver.Resolve<DbContext>(connectionStringResolver);
            _dbContext.Database.Connection.Open();
            _stopwatch = new Stopwatch();
#if DEBUG
            Console.WriteLine($"DbConnection:{_dbContext?.Database.Connection.GetHashCode()}");
#endif
        }

        public override DbContext DbContext => _dbContext;
        public DbTransaction CurrentTransaction { get; private set; }
        public override void Begin()
        {
            CurrentTransaction = _dbContext.Database.Connection.BeginTransaction();
            _stopwatch.Restart();
        }

        public override void Complete()
        {
            try
            {
                CurrentTransaction.Commit();
                IsSucceed = true;
                _stopwatch.Stop();
            }
            catch (Exception exception)
            {
                _stopwatch.Stop();
                try
                {
                    IsSucceed = false;
                    CurrentTransaction.Rollback();
                    OnFailed(_stopwatch.Elapsed, exception);
                }
                catch (Exception rollBackexception)
                {
                    OnFailed(_stopwatch.Elapsed, rollBackexception);
                }
            }
        }

        public override void Dispose()
        {
            _dbContext.Database.Connection.Close();
            _dbContext.Dispose();
        }
    }
}
