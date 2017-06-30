using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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

        public IDbConnection DbConnection { get; private set; }

        public IRepository<TEntity> SetDbConnection(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
            return this;
        }

        protected virtual void ValidateConnection()
        {
            if (DbConnection == null)
            {
                throw new ArgumentNullException($"DbConnection is empty");
            }
        }
        
        public abstract bool Insert(TEntity entity);

        public abstract Task<bool> InsertAsync(TEntity entity);

        public abstract bool Update(TEntity data);

        public abstract Task<bool> UpdateAsync(TEntity entity);

        public abstract bool Remove(TEntity condition);

        public abstract Task<bool> RemoveAsync(TEntity condition);

        public abstract TEntity Get(string sql, object parameterObject = null);

        public abstract Task<TEntity> GetAsync(string sql, object parameterObject = null);
        public abstract TEntity Get(dynamic id);
        public abstract Task<TEntity> GetAsync(dynamic id);

        public abstract IList<TEntity> GetList(string sql, object parameterObject = null);

        public abstract Task<IList<TEntity>> GetListAsync(string sql, object parameterObject = null);
        public abstract IList<TEntity> GetAll();

        public abstract Task<IList<TEntity>> GetAllAsync();

        public abstract Paging<TEntity> Paging(string whereSql, string orderBy, object parameterObjects, int pageIndex, int pageSize);
        public abstract Task<Paging<TEntity>> PagingAsync(string whereSql, string orderBy, object parameterObjects, int pageIndex, int pageSize);
    }
}
