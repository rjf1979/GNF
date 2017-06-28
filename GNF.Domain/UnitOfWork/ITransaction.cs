using System;
using System.Threading.Tasks;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// 事务接口
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// 开始一个事务
        /// </summary>
        void BeginTran();

        void RollbackTran();

        /// <summary>
        /// 完成事务
        /// </summary>
        void CompleteTran();
    }
}
