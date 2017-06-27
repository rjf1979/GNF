using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GNF.Domain.Entities;

namespace GNF.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public abstract IList<TEntity> GetAll();

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(GetAll());
        }

        public abstract IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        public virtual async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(GetList(predicate));
        }

        public abstract TEntity Get(TPrimaryKey id);

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await Task.FromResult(Get(id));
        }

        public abstract bool Exists(TEntity entity);

        public virtual async Task<bool> ExistsAsync(TEntity entity)
        {
            return await Task.FromResult(Exists(entity));
        }

        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);

        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(Single(predicate));
        }

        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(FirstOrDefault(predicate));
        }

        public abstract TEntity Insert(TEntity entity);

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await Task.FromResult(Insert(entity));
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            if (Exists(entity))
            {
                return Update(entity);
            }
            return Insert(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            if (await ExistsAsync(entity))
            {
                return await UpdateAsync(entity);
            }
            return await InsertAsync(entity);
        }

        public abstract TEntity Update(TEntity entity);

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public abstract bool Delete(TEntity entity);

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            return await Task.FromResult(Delete(entity));
        }

        public abstract bool Delete(TPrimaryKey id);

        public virtual async Task<bool> DeleteAsync(TPrimaryKey id)
        {
            return await Task.FromResult(Delete(id));
        }

        public abstract bool Delete(Expression<Func<TEntity, bool>> predicate);

        public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(Delete(predicate));
        }

        public abstract long Count();

        public virtual async Task<long> CountAsync()
        {
            return await Task.FromResult(Count());
        }

        public abstract long Count(Expression<Func<TEntity, bool>> predicate);

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(Count(predicate));
        }
    }
}
