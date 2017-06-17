using System;

namespace GNF.Cache
{
    /// <summary>
    /// 缓存项接口
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface ICacheItemOfTValue<out TValue>:ICacheItem
    {
        string Key { get; }
        TValue Value { get; }
        DateTime ExpireTime { get; }
        bool IsExpired { get; }
    }
}
