using System;
using System.Threading.Tasks;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// Defines a unit of work.
    /// </summary>
    public interface IUnitOfWork<out TDbContext> : IActiveUnitOfWork<TDbContext>,IDisposable
    {
        /// <summary>
        /// 工作单元的唯一ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 是否工作单元执行成功
        /// </summary>
        bool IsSucceed { get; }

        /// <summary>
        /// 开始一个工作单元，如果存在事务，则执行开始事务
        /// </summary>
        void Begin();

        /// <summary>
        /// 完成工作单元，如果存在事务，则执行完成事务
        /// </summary>
        void Complete();
    }
}
