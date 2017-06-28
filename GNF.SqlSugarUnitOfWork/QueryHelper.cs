using System;
using System.Collections.Generic;
using SqlSugar;

namespace GNF.SqlSugarUnitOfWork
{
    public class QueryHelper
    {
        public static IList<TEntity> ToList<TEntity>(ISugarQueryable<TEntity> queryable,bool isWithNoLock)
        {
            return isWithNoLock ? queryable.With(SqlWith.NoLock).ToList() : queryable.ToList();
        }

        public static TEntity Single<TEntity>(ISugarQueryable<TEntity> queryable, bool isWithNoLock)
        {
            return isWithNoLock ? queryable.With(SqlWith.NoLock).ToList() : queryable.ToList();
        }
    }
}
