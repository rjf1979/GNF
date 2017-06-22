using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public string Id { get; }

        /// <summary>
        /// Gets the connection string resolver.
        /// </summary>
        protected IConnectionStringResolver ConnectionStringResolver { get; }

        public event EventHandler<UnitOfWorkCompleteEventArgs> Completed;
        public event EventHandler<UnitOfWorkExceptionEventArgs> Failed;
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Gets a value indicates that this unit of work is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Is this unit of work successfully completed.
        /// </summary>
        public bool IsSuccess { get; protected set; }

        /// <summary>
        /// A reference to the exception if this unit of work failed.
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UnitOfWorkBase(IConnectionStringResolver connectionStringResolver)
        {
            ConnectionStringResolver = connectionStringResolver;
            _stopwatch = new Stopwatch();
            Id = Guid.NewGuid().ToString("N");
        }

        /// <inheritdoc/>
        public virtual void Begin()
        {
            _stopwatch.Start();
        }

        /// <inheritdoc/>
        public abstract void SaveChanges();

        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        /// <inheritdoc/>
        public void Complete()
        {
            try
            {
                CompleteUow();
                IsSuccess = true;
                _stopwatch.Stop();
                OnCompleted(_stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                _exception = ex;
                OnFailed(_stopwatch.Elapsed, _exception);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task CompleteAsync()
        {
            try
            {
                await CompleteUowAsync();
                IsSuccess = true;
                _stopwatch.Stop();
                OnCompleted(_stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                _exception = ex;
                OnFailed(_stopwatch.Elapsed, _exception);
                throw;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            IsDisposed = true;
            DisposeUow();
        }

        /// <summary>
        /// Can be implemented by derived classes to start UOW.
        /// </summary>
        protected virtual void BeginUnitOfWork()
        {

        }

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnCompleted(TimeSpan executeTimeSpan)
        {
            Completed?.Invoke(this,new UnitOfWorkCompleteEventArgs(executeTimeSpan));
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
