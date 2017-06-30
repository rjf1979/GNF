using System.Data;
using GNF.Domain.Entities;

namespace GNF.DapperUow.Repositories
{
    /// <summary>
    /// Represents that the implemented classes are repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root on which the repository operations
    /// should be performed.</typeparam>
    public interface IRepositoryWithTransaction<TEntity>:IRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// DB事务对象
        /// </summary>
        IDbTransaction DbTransaction { get; }

        /// <summary>
        /// 设置DB事务对象，设置后ConnectionString会为空
        /// </summary>
        /// <param name="dbTransaction"></param>
        IRepositoryWithTransaction<TEntity> SetDbTransaction(IDbTransaction dbTransaction);
    }
}
