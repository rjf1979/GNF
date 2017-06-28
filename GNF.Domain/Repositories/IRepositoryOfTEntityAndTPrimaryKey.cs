using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GNF.Domain.Entities;

namespace GNF.Domain.Repositories
{
    public interface IRepository<TEntity, in TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 获取所有实体对象集合
        /// </summary>
        IList<TEntity> GetAll(bool isWithNoLock = true);

        /// <summary>
        /// 获取所有实体对象集合
        /// </summary>
        Task<IList<TEntity>> GetAllAsync(bool isWithNoLock = true);

        /// <summary>
        /// 指定筛选条件获取实体集合
        /// </summary>
        IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        /// <summary>
        /// 指定筛选条件获取实体集合
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <param name="isWithNoLock"></param>
        /// <returns>List of all entities</returns>
        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        /// <summary>
        /// 指定主键ID获取实体对象
        /// </summary>
        TEntity Get(TPrimaryKey id, bool isWithNoLock = true);

        /// <summary>
        /// 指定主键ID获取实体对象
        /// </summary>
        Task<TEntity> GetAsync(TPrimaryKey id, bool isWithNoLock = true);

        bool Exists(TEntity entity, bool isWithNoLock = true);

        Task<bool> ExistsAsync(TEntity entity, bool isWithNoLock = true);
        /// <summary>
        /// 指定筛选条件获取单个实体对象
        /// </summary>
        TEntity Single(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        /// <summary>
        /// 指定筛选条件获取单个实体对象
        /// </summary>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        /// <summary>
        /// 指定筛选条件获取首个实体对象
        /// </summary>
        TEntity First(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        /// <summary>
        /// 指定筛选条件获取首个实体对象
        /// </summary>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        /// <summary>
        /// 插入一个实体对象，并且返回插入成功后的实体对象【注意：在领域开发内，虽然对象的hashCode不同，视同相同】
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        bool Insert(TEntity entity);

        /// <summary>
        /// 插入一个实体对象，并且返回插入成功后的实体对象【注意：在领域开发内，虽然对象的hashCode不同，视同相同】
        /// </summary>
        Task<bool> InsertAsync(TEntity entity);

        /// <summary>
        /// 插入或更新一个实体对象
        /// </summary>
        bool InsertOrUpdate(TEntity entity);

        /// <summary>
        /// 插入或更新一个实体对象
        /// </summary>
        Task<bool> InsertOrUpdateAsync(TEntity entity);

        /// <summary>
        /// 更新一个实体对象
        /// </summary>
        bool Update(TEntity entity);

        /// <summary>
        /// 更新一个实体对象
        /// </summary>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        bool Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        bool Delete(TPrimaryKey id);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        Task<bool> DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// 指定筛选表达式来删除实体对象
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        bool Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 指定筛选表达式来删除实体对象
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 统计数据量
        /// </summary>
        /// <returns>Count of entities</returns>
        long Count(bool isWithNoLock = true);

        /// <summary>
        /// 统计数据量
        /// </summary>
        Task<long> CountAsync(bool isWithNoLock = true);

        /// <summary>
        /// 统计数据量
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <param name="isWithNoLock"></param>
        long Count(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        /// <summary>
        /// 统计数据量
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <param name="isWithNoLock"></param>
        /// <returns>Count of entities</returns>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);
    }
}
