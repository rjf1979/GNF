using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using Dapper.Contrib.Extensions;
using GNF.Common.Utility;

namespace GNF.DapperUow
{
    public static class ConnectionExtension
    {
        public static void QueryPaging<TEntity>(this IDbConnection connection, ref Paging<TEntity> paging, object paramterObjects = null, int? commandTimeout = null)
        {
            var sql = string.Format("SELECT {0} FROM (SELECT ROW_NUMBER() OVER ({1}) AS RowNumber, {0} FROM {2}{3}) AS Total WHERE RowNumber >= {4} AND RowNumber <= {5}", paging.Columns, paging.OrderBy, paging.Table, paging.WhereSql, (paging.PageIndex - 1) * paging.PageSize + 1, paging.PageIndex * paging.PageSize);
            var datas = connection.Query<TEntity>(sql, paramterObjects, null, true, commandTimeout).ToList();
            var countSql = $"SELECT COUNT(0) FROM {paging.Table} {paging.WhereSql} ";
            var total = connection.QueryFirstOrDefault<int>(countSql, paramterObjects);
            paging.FillQueryData(total, datas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static bool LockRow<TEntity>(this IDbConnection connection, dynamic id, IDbTransaction transaction, int commandTimeout = 3)
        {
            var type = typeof(TEntity);
            var keyName = GetKeyName(type);
            var table = GetTable(type);
            string sql = $"UPDATE {table} SET {keyName}=@id WHERE {keyName} = @id";
            var dynParms = new DynamicParameters();
            dynParms.Add("@id", id);
            return connection.Execute(sql, dynParms, transaction, commandTimeout) > 0;
        }

        public static string GetKeyName(Type type)
        {
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Public))
            {
                var explicitKeyAttribute = propertyInfo.GetCustomAttribute<ExplicitKeyAttribute>();
                if (explicitKeyAttribute != null) return propertyInfo.Name;
                var keyAttribute = propertyInfo.GetCustomAttribute<KeyAttribute>();
                if (keyAttribute != null) return propertyInfo.Name;
            }
            return string.Empty;
        }

        public static string GetTable(Type type)
        {
            var t = AttributeUtility.GetAttribute<TableAttribute>(type);
            return t != null ? t.Name : string.Empty;
        }

        public static IEnumerable<string> GetColumns(Type type)
        {
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Public))
            {
                var computedAttr = propertyInfo.GetCustomAttribute<ComputedAttribute>();
                if (computedAttr != null) continue;
                yield return propertyInfo.Name;
            }
        }
    }

    public class Paging<TEntity>
    {
        ///  <summary>
        /// 
        ///  </summary>
        ///  <param name="pageIndex"></param>
        ///  <param name="pageSize"></param>
        /// <param name="whereSql"></param>
        /// <param name="orderBy"></param>
        public Paging(int pageIndex, int pageSize, string whereSql, string orderBy)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Table = ConnectionExtension.GetTable(typeof(TEntity));
            Columns = ConnectionExtension.GetColumns(typeof(TEntity)).ToArray();
            WhereSql = whereSql.Trim().StartsWith("WHERE", StringComparison.CurrentCultureIgnoreCase) ? " " + whereSql + " " : " WHERE " + WhereSql + " ";
            OrderBy = orderBy.Trim().StartsWith("ORDER BY", StringComparison.CurrentCultureIgnoreCase) ? " " + orderBy + " " : " ORDER BY " + orderBy + " ";
        }

        internal void FillQueryData(int recordCount, IList<TEntity> entities)
        {
            RecordCount = recordCount;
            Items = entities;
            PageCount = new Func<long>(delegate
            {
                var pages = RecordCount / PageSize;
                if (RecordCount % PageSize != 0)
                {
                    pages = pages + 1;
                }
                if (PageIndex > pages)
                {
                    PageIndex = pages;
                }
                return pages;
            }).Invoke();
        }

        /// <summary>
        ///
        /// </summary>
        public long PageIndex { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public long PageSize { get; }

        /// <summary>
        ///
        /// </summary>
        public long PageCount { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public long RecordCount { get; private set; }
        public string Table { get; }
        public string[] Columns { get; }
        /// <summary>
        /// 筛选条件
        /// </summary>
        public string WhereSql { get; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string OrderBy { get; }

        /// <summary>
        ///
        /// </summary>
        public IList<TEntity> Items { get; private set; }
    }
}
