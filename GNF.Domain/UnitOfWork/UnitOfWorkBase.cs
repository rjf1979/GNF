using System;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// </summary>
    public abstract class UnitOfWorkBase<TDbContext> : IUnitOfWork<TDbContext>
    {
        public string Id { get; }
        public event EventHandler<UnitOfWorkCompleteEventArgs> Completed;
        public event EventHandler<UnitOfWorkExceptionEventArgs> Failed;
        //private readonly Stopwatch _stopwatch;
        //private Exception _exception;

        protected UnitOfWorkBase(IConnectionStringResolver connectionStringResolver)
        {
            ConnectionStringResolver = connectionStringResolver;
            //_stopwatch = new Stopwatch();
            Id = Guid.NewGuid().ToString("N");
        }
        public IConnectionStringResolver ConnectionStringResolver { get; }

        public abstract TDbContext DbContext { get; }

        public abstract void Begin();
        
        public bool IsSucceed { get; protected set; }
        public Exception Exception { get; protected set; }

        public abstract void Complete();

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
