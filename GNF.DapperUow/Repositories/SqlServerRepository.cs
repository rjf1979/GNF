using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using GNF.Domain.Entities;

namespace GNF.DapperUow.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SqlServerRepository<TEntity> : RepositoryWithTransaction<TEntity>
        where TEntity : class, IEntity
    {
        private IDbConnection OpenDbConnection()
        {
            var conn = DbTransaction != null ? DbTransaction.Connection : DbConnection;
            if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Closed) conn.Open();
            return conn;
        }

        private void CloseConnection(IDbConnection conn)
        {
            if (DbTransaction == null)
            {
                if (DbConnection.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override bool Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = conn.Insert(entity, DbTransaction) > 0;
            CloseConnection(conn);
            return value;
        }

        public override async Task<bool> InsertAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = await conn.InsertAsync(entity, DbTransaction) > 0;
            CloseConnection(conn);
            return value;
        }

        public override bool Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = conn.Update(entity, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = await conn.UpdateAsync(entity, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        public override bool Remove(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = conn.Delete(entity, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        public override async Task<bool> RemoveAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = await conn.DeleteAsync(entity, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        public override TEntity Get(string sql, object parameterObject = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = conn.QueryFirstOrDefault<TEntity>(sql, parameterObject, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        public override async Task<TEntity> GetAsync(string sql, object parameterObject = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = await conn.QueryFirstOrDefaultAsync<TEntity>(sql, parameterObject, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        public override TEntity Get(dynamic id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = SqlMapperExtensions.Get<TEntity>(conn, id, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        public override async Task<TEntity> GetAsync(dynamic id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            ValidateConnection();
            var conn = OpenDbConnection();
            var value = await SqlMapperExtensions.GetAsync<TEntity>(conn, id, DbTransaction);
            CloseConnection(conn);
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public override IList<TEntity> GetList(string sql, object parameterObject = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));
            ValidateConnection();
            var conn = OpenDbConnection();
            var values = conn.Query<TEntity>(sql, parameterObject, DbTransaction).ToList();
            CloseConnection(conn);
            return values;
        }

        public override async Task<IList<TEntity>> GetListAsync(string sql, object parameterObject = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));
            ValidateConnection();
            var conn = OpenDbConnection();
            var values = await conn.QueryAsync<TEntity>(sql, parameterObject, DbTransaction);
            CloseConnection(conn);
            return values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IList<TEntity> GetAll()
        {
            ValidateConnection();
            var conn = OpenDbConnection();
            var list = conn.GetAll<TEntity>().ToList();
            CloseConnection(conn);
            return list;
        }

        public override async Task<IList<TEntity>> GetAllAsync()
        {
            ValidateConnection();
            var conn = OpenDbConnection();
            var list = await conn.GetAllAsync<TEntity>();
            CloseConnection(conn);
            return list.ToList();
        }

        public override Paging<TEntity> Paging(string whereSql, string orderBy, object parameterObjects, int pageIndex, int pageSize)
        {
            ValidateConnection();
            var conn = OpenDbConnection();
            Paging<TEntity> pagedList = new Paging<TEntity>(pageIndex, pageSize, whereSql, orderBy);
            conn.QueryPaging(ref pagedList, parameterObjects);
            CloseConnection(conn);
            return pagedList;
        }

        public override async Task<Paging<TEntity>> PagingAsync(string whereSql, string orderBy, object parameterObjects, int pageIndex, int pageSize)
        {
            ValidateConnection();
            var conn = OpenDbConnection();
            Paging<TEntity> pagedList = new Paging<TEntity>(pageIndex, pageSize, whereSql, orderBy);
            pagedList = await conn.QueryPagingAsync(pagedList, parameterObjects);
            CloseConnection(conn);
            return pagedList;
        }
    }
}
