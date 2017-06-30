using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using GNF.Domain.Entities;

namespace GNF.DapperUow.Repositories
{
    /// <summary>
    /// Represents that the implemented classes are repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root on which the repository operations
    /// should be performed.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// DB事务对象
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// 设置DB事务对象，设置后ConnectionString会为空
        /// </summary>
        /// <param name="dbConnection"></param>
        IRepository<TEntity> SetDbConnection(IDbConnection dbConnection);

        bool Insert(TEntity entity);

        Task<bool> InsertAsync(TEntity entity);

        bool Update(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        bool Remove(TEntity condition);

        Task<bool> RemoveAsync(TEntity condition);

        TEntity Get(string sql,object parameterObject = null);

        Task<TEntity> GetAsync(string sql, object parameterObject = null);

        TEntity Get(dynamic id);

        Task<TEntity> GetAsync(dynamic id);

        ///// <summary>
        ///// 指定Id，获取一个实体对象；如果要求附带UPDLOCK更新锁，就能防止脏读数据
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="isUpdateLock"></param>
        ///// <returns></returns>
        //TEntity GetById(dynamic id,bool isUpdateLock);

        IList<TEntity> GetList(string sql, object parameterObject = null);

        Task<IList<TEntity>> GetListAsync(string sql, object parameterObject = null);

        IList<TEntity> GetAll();

        Task<IList<TEntity>> GetAllAsync();

        Paging<TEntity> Paging(string whereSql, string orderBy,object parameterObjects, int pageIndex, int pageSize);

        Task<Paging<TEntity>> PagingAsync(string whereSql, string orderBy, object parameterObjects, int pageIndex, int pageSize);
    }
}
