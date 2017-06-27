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
        public event EventHandler<UnitOfWorkCompleteEventArgs> Completed;
        public event EventHandler<UnitOfWorkExceptionEventArgs> Failed;
        private readonly Stopwatch _stopwatch;
        private Exception _exception;
        private readonly IDbContext _dbContext;

        protected UnitOfWorkBase(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _stopwatch = new Stopwatch();
            Id = Guid.NewGuid().ToString("N");
        }

        /// <inheritdoc/>
        public virtual void Begin()
        {
            _stopwatch.Start();
        }

        public bool IsSucceed { get; private set; }
        
        public abstract void SaveChanges();

        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        protected abstract bool CompleteUnitOfWork();

        public void Complete()
        {
            try
            {
                IsSucceed = CompleteUnitOfWork();
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

        protected abstract Task<bool> CompleteUnitOfWorkAsync();

        public async Task CompleteAsync()
        {
            try
            {
                IsSucceed = await CompleteUnitOfWorkAsync();
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
        
        public abstract void Dispose();
        
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
