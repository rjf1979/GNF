using System.Collections.Generic;

namespace GNF.Cache.LocalCache
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<ICacheItem> CacheItems { get; }
    }
}
