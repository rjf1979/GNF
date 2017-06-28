using System;
using System.Diagnostics;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// </summary>
    public abstract class UnitOfWorkBase<TDbClient> : IUnitOfWork<TDbClient>
    {
        public string Id { get; }
        public event EventHandler<UnitOfWorkCompleteEventArgs> Completed;
        public event EventHandler<UnitOfWorkExceptionEventArgs> Failed;
        private readonly Stopwatch _stopwatch;
        private Exception _exception;

        protected UnitOfWorkBase(IConnectionStringResolver connectionStringResolver)
        {
            ConnectionStringResolver = connectionStringResolver;
            _stopwatch = new Stopwatch();
            Id = Guid.NewGuid().ToString("N");
        }
        public IConnectionStringResolver ConnectionStringResolver { get; }

        public abstract IDbContext<TDbClient> DbContext { get; }

        public abstract ITransaction Transaction { get; }

        public virtual void Begin()
        {
            _stopwatch.Start();
            try
            {
                Transaction?.BeginTran();
            }
            catch (Exception exception)
            {
                IsSucceed = false;
                _exception = exception;
                OnFailed(_stopwatch.Elapsed, _exception);
            }
        }

        public void RollBack()
        {
            IsSucceed = false;
            try
            {
                Transaction?.RollbackTran();
                _stopwatch.Stop();
                OnFailed(_stopwatch.Elapsed, null);
            }
            catch (Exception exception)
            {
                _exception = exception;
                OnFailed(_stopwatch.Elapsed, _exception);
            }
        }

        public bool IsSucceed { get; private set; }

        public virtual void Complete()
        {
            try
            {
                Transaction?.CompleteTran();
                IsSucceed = true;
                _stopwatch.Stop();
                OnCompleted(_stopwatch.Elapsed);
            }
            catch (Exception exception)
            {
                _exception = exception;
                IsSucceed = false;
                OnFailed(_stopwatch.Elapsed, _exception);
            }
        }

        public abstract void Dispose();

        protected virtual void OnCompleted(TimeSpan executeTimeSpan)
        {
            Completed?.Invoke(this, new UnitOfWorkCompleteEventArgs(executeTimeSpan));
        }

        protected virtual void OnFailed(TimeSpan executeTimeSpan, Exception exception)
        {
            Failed?.Invoke(this, new UnitOfWorkExceptionEventArgs(executeTimeSpan, exception));
        }

        public override string ToString()
        {
            return $"[UnitOfWork {Id}]";
        }
    }
}
