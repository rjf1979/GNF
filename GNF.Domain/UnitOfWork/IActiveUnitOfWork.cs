using System;
using System.Threading.Tasks;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// 可用的工作单元
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// This event is raised when this UOW is successfully completed.
        /// </summary>
        event EventHandler<UnitOfWorkCompleteEventArgs> Completed;

        /// <summary>
        /// This event is raised when this UOW is failed.
        /// </summary>
        event EventHandler<UnitOfWorkExceptionEventArgs> Failed;

        /// <summary>
        /// 是否工作单元执行成功
        /// </summary>
        bool IsSucceed { get; }

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// This method may be called to apply changes whenever needed.
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// since all changes saved at end of a unit of work automatically.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// This method may be called to apply changes whenever needed.
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// since all changes saved at end of a unit of work automatically.
        /// </summary>
        Task SaveChangesAsync();
    }
}
