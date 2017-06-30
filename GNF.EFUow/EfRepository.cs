using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GNF.Domain.Entities;
using GNF.Domain.Repositories;

namespace GNF.EFUow
{
    public class EfRepository<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>, new()
    {
        protected readonly EfDbContext<TEntity, TPrimaryKey> DbContext;

        public EfRepository(EfDbContext<TEntity, TPrimaryKey> dbContext)
        {
            DbContext = dbContext;
        }

        public override IList<TEntity> GetAll(bool isWithNoLock = true)
        {
            return DbContext.Entities.ToList();
        }

        public override IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            //var query = DbContext.Current.Queryable<TEntity>().Where(predicate);
            //return QueryHelper.ToList(query, isWithNoLock);
            return new List<TEntity>();
        }

        public override TEntity Get(TPrimaryKey id, bool isWithNoLock = true)
        {
            return null;
            //var query = DbContext.Current.Queryable<TEntity>();
            //return isWithNoLock ? query.With(SqlWith.NoLock).InSingle(id) : query.InSingle(id);
        }

        public override bool Exists(TEntity entity, bool isWithNoLock = true)
        {
            return false;
            //var objEntity = entity as IEntity<TPrimaryKey>;
            //return objEntity != null && Get(objEntity.Id) != null;
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            return null;
            //var query = DbContext.Current.Queryable<TEntity>();
            //return isWithNoLock ? query.With(SqlWith.NoLock).Single(predicate) : query.Single(predicate);
        }

        public override TEntity First(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            return null;
            //var query = DbContext.Current.Queryable<TEntity>();
            //return isWithNoLock ? query.With(SqlWith.NoLock).First(predicate) : query.First(predicate);
        }

        public override bool Insert(TEntity entity)
        {
            return false;
            //DbContext.Current.Insertable(entity).ExecuteCommand();
            //return true;
        }

        public override bool Update(TEntity entity)
        {
            return false;
            //return DbContext.Current.Updateable(entity).ExecuteCommand() > 0;
        }

        public override bool Delete(TEntity entity)
        {
            return false;
            //var objEntity = entity as IEntity<TPrimaryKey>;
            //return Delete(objEntity.Id);
        }

        public override bool Delete(TPrimaryKey id)
        {
            return false;
            //return DbContext.Current.Deleteable<TEntity>().In(id).ExecuteCommand() > 0;
        }

        public override bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return false;
            //return DbContext.Current.Deleteable<TEntity>().Where(predicate).ExecuteCommand() > 0;
        }

        public override long Count(bool isWithNoLock = true)
        {
            return 0;
            //var query = DbContext.Current.Queryable<TEntity>();
            //return isWithNoLock ? query.With(SqlWith.NoLock).Count() : query.Count();
        }

        public override long Count(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            return 0;
            //var query = DbContext.Current.Queryable<TEntity>();
            //return isWithNoLock ? query.With(SqlWith.NoLock).Where(predicate).Count() : query.Where(predicate).Count();
        }
    }
}
