using System;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// 可用的工作单元
    /// </summary>
    public interface IActiveUnitOfWork<out TDbClient>
    {
        /// <summary>
        /// This event is raised when this UOW is successfully completed.
        /// </summary>
        event EventHandler<UnitOfWorkCompleteEventArgs> Completed;

        /// <summary>
        /// This event is raised when this UOW is failed.
        /// </summary>
        event EventHandler<UnitOfWorkExceptionEventArgs> Failed;
        IConnectionStringResolver ConnectionStringResolver { get; }
        IDbContext<TDbClient> DbContext { get; }
    }
}
