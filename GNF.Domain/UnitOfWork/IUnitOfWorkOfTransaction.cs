using System;
using System.Threading.Tasks;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// 带有事务的工作单元接口
    /// </summary>
    public interface IUnitOfWorkOfTransaction : IDisposable
    {
        /// <summary>
        /// Completes this unit of work.
        /// It saves all changes and commit transaction if exists.
        /// </summary>
        void Complete();

        /// <summary>
        /// Completes this unit of work.
        /// It saves all changes and commit transaction if exists.
        /// </summary>
        Task CompleteAsync();
    }
}
