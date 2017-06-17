namespace GNF.Cache.LocalCache.Event
{
    /// <summary>
    /// 缓存事件
    /// </summary>
    public abstract class CacheEvent
    {
        /// <summary>
        /// 添加缓存中的字典后引发此事件
        /// </summary>
        public event CacheEventHandle OnSaved;

        /// <summary>
        /// 删除缓存中的字典后引发此事件
        /// </summary>
        public event CacheEventHandle OnRemoved;


        private void DoEvent(CacheEventHandle cacheEventHandle, ICacheEventArgs cacheEventArgs)
        {
            var dlist = cacheEventHandle.GetInvocationList();
            foreach (var @delegate in dlist)
            {
                var item = (CacheEventHandle)@delegate;
                item.BeginInvoke(this, cacheEventArgs, null, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheItems"></param>
        protected void DoSavedEvent(params ICacheItem[] cacheItems)
        {
            if (OnSaved != null)
            {
                ICacheEventArgs cacheEventArgs = new CacheEventArgs(cacheItems);
                DoEvent(OnSaved, cacheEventArgs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheItem"></param>
        protected void DoRemovedEvent(ICacheItem cacheItem)
        {
            if (OnRemoved != null)
            {
                ICacheEventArgs cacheEventArgs = new CacheEventArgs(cacheItem);
                DoEvent(OnRemoved, cacheEventArgs);
            }
        }

        /// <summary>
        /// 清除事件
        /// </summary>
        protected void ClearEvent()
        {
            OnSaved = null;
            OnRemoved = null;
        }
    }
}
