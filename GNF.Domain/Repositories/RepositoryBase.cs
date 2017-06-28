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
        public abstract IList<TEntity> GetAll(bool isWithNoLock = true);

        public virtual async Task<IList<TEntity>> GetAllAsync(bool isWithNoLock = true)
        {
            return await Task.FromResult(GetAll(isWithNoLock));
        }

        public abstract IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        public virtual async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            return await Task.FromResult(GetList(predicate, isWithNoLock));
        }

        public abstract TEntity Get(TPrimaryKey id, bool isWithNoLock = true);

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id, bool isWithNoLock = true)
        {
            return await Task.FromResult(Get(id, isWithNoLock));
        }

        public abstract bool Exists(TEntity entity, bool isWithNoLock = true);

        public virtual async Task<bool> ExistsAsync(TEntity entity, bool isWithNoLock = true)
        {
            return await Task.FromResult(Exists(entity, isWithNoLock));
        }

        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            return await Task.FromResult(Single(predicate, isWithNoLock));
        }

        public abstract TEntity First(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            return await Task.FromResult(First(predicate, isWithNoLock));
        }

        public abstract bool Insert(TEntity entity);

        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            return await Task.FromResult(Insert(entity));
        }

        public virtual bool InsertOrUpdate(TEntity entity)
        {
            if (Exists(entity, false))
            {
                return Update(entity);
            }
            return Insert(entity);
        }

        public virtual async Task<bool> InsertOrUpdateAsync(TEntity entity)
        {
            if (await ExistsAsync(entity, false))
            {
                return await UpdateAsync(entity);
            }
            return await InsertAsync(entity);
        }

        public abstract bool Update(TEntity entity);

        public virtual Task<bool> UpdateAsync(TEntity entity)
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

        public abstract long Count(bool isWithNoLock = true);

        public virtual async Task<long> CountAsync(bool isWithNoLock = true)
        {
            return await Task.FromResult(Count(isWithNoLock));
        }

        public abstract long Count(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true);

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            return await Task.FromResult(Count(predicate, isWithNoLock));
        }
    }
}
