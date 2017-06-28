using System;
using System.Threading.Tasks;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// Defines a unit of work.
    /// </summary>
    public interface IUnitOfWork<out TDbClient> : IActiveUnitOfWork<TDbClient>,IDisposable
    {
        /// <summary>
        /// 工作单元的唯一ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 是否工作单元执行成功
        /// </summary>
        bool IsSucceed { get; }

        ITransaction Transaction { get; }

        /// <summary>
        /// 开始一个工作单元，如果存在事务，则执行开始事务
        /// </summary>
        void Begin();

        /// <summary>
        /// 工作单元操作回滚
        /// </summary>
        void RollBack();

        /// <summary>
        /// 完成工作单元，如果存在事务，则执行完成事务
        /// </summary>
        void Complete();
    }
}
