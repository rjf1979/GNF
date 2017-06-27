using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GNF.Domain.Entities;

namespace GNF.Domain.Repositories
{
    public interface IRepository<TEntity, in TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        //#region Select/Get/Query

        /// <summary>
        /// 获取所有实体对象集合
        /// </summary>
        IList<TEntity> GetAll();

        /// <summary>
        /// 获取所有实体对象集合
        /// </summary>
        Task<IList<TEntity>> GetAllAsync();

        /// <summary>
        /// 指定筛选条件获取实体集合
        /// </summary>
        IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 指定筛选条件获取实体集合
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 指定主键ID获取实体对象
        /// </summary>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// 指定主键ID获取实体对象
        /// </summary>
        Task<TEntity> GetAsync(TPrimaryKey id);

        bool Exists(TEntity entity);

        Task<bool> ExistsAsync(TEntity entity);
        /// <summary>
        /// 指定筛选条件获取单个实体对象
        /// </summary>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 指定筛选条件获取单个实体对象
        /// </summary>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        ///// <summary>
        ///// 指定筛选条件获取单个实体对象
        ///// </summary>
        //TEntity FirstOrDefault(TPrimaryKey id);

        ///// <summary>
        ///// 指定筛选条件获取单个实体对象
        ///// </summary>
        //Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        /// <summary>
        /// 指定筛选条件获取首个实体对象
        /// </summary>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 指定筛选条件获取首个实体对象
        /// </summary>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 插入一个实体对象，并且返回插入成功后的实体对象【注意：在领域开发内，虽然对象的hashCode不同，视同相同】
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入一个实体对象，并且返回插入成功后的实体对象【注意：在领域开发内，虽然对象的hashCode不同，视同相同】
        /// </summary>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 插入或更新一个实体对象
        /// </summary>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>
        /// 插入或更新一个实体对象
        /// </summary>
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        /// <summary>
        /// 更新一个实体对象
        /// </summary>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新一个实体对象
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity);

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
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        long Count();

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<long> CountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        long Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
