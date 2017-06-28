﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GNF.Domain.Entities;
using GNF.Domain.Repositories;
using GNF.Domain.UnitOfWork;
using SqlSugar;

namespace GNF.SqlSugarUnitOfWork
{
    public class SqlRepository<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        private readonly IDbContext<SqlSugarClient> _dbContext;

        public SqlRepository(IDbContext<SqlSugarClient> dbContext)
        {
            _dbContext = dbContext;
        }

        public override IList<TEntity> GetAll(bool isWithNoLock = true)
        {
            var query = _dbContext.Context.Queryable<TEntity>();
            return QueryHelper.ToList(query, isWithNoLock);
        }

        public override IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            var query = _dbContext.Context.Queryable<TEntity>().Where(predicate);
            return QueryHelper.ToList(query, isWithNoLock);
        }

        public override TEntity Get(TPrimaryKey id, bool isWithNoLock = true)
        {
            var query = _dbContext.Context.Queryable<TEntity>();
            return isWithNoLock ? query.With(SqlWith.NoLock).InSingle(id) : query.InSingle(id);
        }

        public override bool Exists(TEntity entity, bool isWithNoLock = true)
        {
            var objEntity = entity as IEntity<TPrimaryKey>;
            return objEntity != null && Get(objEntity.Id) != null;
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            var query = _dbContext.Context.Queryable<TEntity>();
            return isWithNoLock ? query.With(SqlWith.NoLock).Single(predicate) : query.Single(predicate);
        }

        public override TEntity First(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            var query = _dbContext.Context.Queryable<TEntity>();
            return isWithNoLock ? query.With(SqlWith.NoLock).First(predicate) : query.First(predicate);
        }

        public override bool Insert(TEntity entity)
        {
            _dbContext.Context.Insertable(entity).ExecuteCommand();
            return true;
        }

        public override bool Update(TEntity entity)
        {
            return _dbContext.Context.Updateable(entity).ExecuteCommand() > 0;
        }

        public override bool Delete(TEntity entity)
        {
            var objEntity = entity as IEntity<TPrimaryKey>;
            return Delete(objEntity.Id);
        }

        public override bool Delete(TPrimaryKey id)
        {
            return _dbContext.Context.Deleteable<TEntity>().In(id).ExecuteCommand() > 0;
        }

        public override bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Context.Deleteable<TEntity>().Where(predicate).ExecuteCommand() > 0;
        }

        public override long Count(bool isWithNoLock = true)
        {
            var query = _dbContext.Context.Queryable<TEntity>();
            return isWithNoLock ? query.With(SqlWith.NoLock).Count() : query.Count();
        }

        public override long Count(Expression<Func<TEntity, bool>> predicate, bool isWithNoLock = true)
        {
            var query = _dbContext.Context.Queryable<TEntity>();
            return isWithNoLock ? query.With(SqlWith.NoLock).Where(predicate).Count() : query.Where(predicate).Count();
        }
    }
}
