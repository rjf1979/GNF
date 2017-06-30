using System;
using System.Data;
using GNF.Domain.Entities;

namespace GNF.DapperUow
{
    /// <summary>
    /// 表示所有集成于该接口的类型都是Unit Of Work的一种实现。
    /// </summary>
    /// <remarks>有关Unit Of Work的详细信息，请参见UnitOfWork模式：http://martinfowler.com/eaaCatalog/unitOfWork.html。
    /// </remarks>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表述了当前的Unit Of Work事务是否已被提交。
        /// </summary>
        bool Committed { get; }
       
        /// <summary>
        /// 提交当前的Unit Of Work事务。
        /// </summary>
        bool Commit();

        /// <summary>
        /// 回滚当前的Unit Of Work事务。
        /// </summary>
        void Rollback();

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        IDbTransaction DbTransaction { get; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id">数据ID</param>
        /// <param name="timeout">超时时间，单位：秒</param>
        /// <returns></returns>
        bool LockObject<TEntity>(int timeout, params object[] id)
            where TEntity : IEntity;

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        void RegisterAdded<TEntity>(TEntity obj)
            where TEntity : IEntity;

        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        void RegisterModified<TEntity>(TEntity obj)
            where TEntity : IEntity;

        /// <summary>
        /// Registers a removed object to the repository context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        void RegisterRemoved<TEntity>(TEntity obj)
            where TEntity : IEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        /// <param name="repositoryItemType"></param>
        /// <param name="operateName"></param>
        void RegisterCustomOperate<TEntity>(TEntity obj, Type repositoryItemType, string operateName)
            where TEntity : IEntity;
    }
}
