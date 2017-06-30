using System;
using GNF.Domain.Entities;

namespace GNF.DapperUow
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class CustomOperate<TData> where TData : IEntity
    {
        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type RepositoryItemType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OperateName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repositoryItemType"></param>
        /// <param name="operateName"></param>
        public CustomOperate(TData data, Type repositoryItemType, string operateName)
        {
            Data = data;
            RepositoryItemType = repositoryItemType;
            OperateName = operateName;
        }
    }
}
