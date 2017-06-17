using System.Collections.Generic;

namespace GNF.Cache
{
    /// <summary>
    /// 分布式缓存接口
    /// </summary>
    interface ICacheSync<TCacheItem> : ICache where TCacheItem : ICacheItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheItem"></param>
        /// <returns></returns>
        bool Save(TCacheItem cacheItem);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheItems"></param>
        /// <returns></returns>
        bool Save(IList<TCacheItem> cacheItems);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TCacheItem Get(string key);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<TCacheItem> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        IList<TValue> GetAllValues<TValue>();
    }
}
