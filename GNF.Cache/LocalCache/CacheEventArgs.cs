using System;
using System.Collections.Generic;

namespace GNF.Cache.LocalCache
{ 
    /// <summary>
    ///
    /// </summary>
    public class CacheEventArgs : EventArgs, ICacheEventArgs
    {
        /// <summary>
        ///
        /// </summary>
        public IEnumerable<ICacheItem> CacheItems { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cacheItems"></param>
        public CacheEventArgs(params ICacheItem[] cacheItems)
        {
            CacheItems = cacheItems;
        }
    }
}