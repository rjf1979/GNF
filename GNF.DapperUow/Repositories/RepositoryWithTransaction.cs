using System;
using System.Data;
using GNF.Domain.Entities;

namespace GNF.DapperUow.Repositories
{
    /// <summary>
    /// Represents the base class for repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root on which the repository operations
    /// should be performed.</typeparam>
    public abstract class RepositoryWithTransaction<TEntity> : Repository<TEntity>, IRepositoryWithTransaction<TEntity>
        where TEntity : IEntity
    {
        private bool _isEnableTransaction;

        /// <summary>
        /// DB事务对象
        /// </summary>
        public IDbTransaction DbTransaction { get; protected set; }

        /// <summary>
        /// 设置DB事务对象，设置后ConnectionString会为空
        /// </summary>
        /// <param name="dbTransaction"></param>
        public IRepositoryWithTransaction<TEntity> SetDbTransaction(IDbTransaction dbTransaction)
        {
            DbTransaction = dbTransaction;
            _isEnableTransaction = true;
            return this;
        }

        /// <summary>
        /// 验证是否是有效连接
        /// </summary>
        protected override void ValidateConnection()
        {
            if (_isEnableTransaction)
            {
                if (DbTransaction == null) throw new ArgumentNullException($"DbTransaction is empty");
            }
            else
            {
                base.ValidateConnection();
            }
        }
    }
}
