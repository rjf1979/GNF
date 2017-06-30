using System;
using System.Collections.Generic;
using System.Data;
using GNF.Domain.Entities;

namespace GNF.DapperUow.Repositories
{
    /// <summary>
    /// Represents the base class for repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root on which the repository operations
    /// should be performed.</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        protected Repository(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        protected Repository()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbConnection DbConnection { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public IRepository<TEntity> SetDbConnection(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void ValidateConnection()
        {
            if (DbConnection == null)
            {
                throw new ArgumentNullException($"DbConnection is empty");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool Add(TEntity data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool Save(TEntity data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public abstract bool Remove(TEntity condition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public abstract TEntity Get(string sql, object parameterObject = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TEntity GetById(dynamic id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isUpdateLock"></param>
        /// <returns></returns>
        public abstract TEntity GetById(dynamic id, bool isUpdateLock);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public abstract IList<TEntity> GetList(string sql, object parameterObject = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IList<TEntity> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameterObjects"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public abstract Paging<TEntity> GetPagedList(string whereSql, string orderBy, object parameterObjects, int pageIndex, int pageSize);
    }
}
