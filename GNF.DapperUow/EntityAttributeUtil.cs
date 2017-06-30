using System.Text;
using Dapper.Contrib.Extensions;
using GNF.Common.Utility;
using GNF.Domain.Entities;

namespace GNF.DapperUow
{
    /// <summary>
    /// 实体特性帮助类
    /// </summary>
    public class EntityAttributeUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static string GetId<TEntity>(TEntity entity) where TEntity : IEntity
        {
            var type = entity.GetType();
            StringBuilder idBuilder =new StringBuilder();
            foreach (var propertyInfo in type.GetProperties())
            {
                var attr = AttributeUtility.GetAttribute<ExplicitKeyAttribute>(propertyInfo,true);
                if (attr != null)
                {
                    if (idBuilder.Length > 0) idBuilder.Append("_");
                    idBuilder.Append(propertyInfo.GetValue(entity, null));
                }
            }
            return idBuilder.ToString();
        }
    }
}
