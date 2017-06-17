using System.Collections.Generic;
using System.Threading.Tasks;
using GNF.Common.Cache;

namespace GNF.Cache
{
    /// <summary>
    /// 缓存异步接口
    /// </summary>
    /// <typeparam name="TCacheItem"></typeparam>
    public interface ICacheAsync<TCacheItem> : ICache where TCacheItem : ICacheItem
    {
        Task<bool> SaveAsync(TCacheItem cacheItem);
        Task<bool> SaveAsync(IList<TCacheItem> cacheItems);
        Task<bool> RemoveAsync(string key);
        Task<TCacheItem> GetAsync(string key);
        Task<IList<TCacheItem>> GetAllAsync();
        Task<IList<TValue>> GetAllValuesAsync<TValue>();
    }
}
