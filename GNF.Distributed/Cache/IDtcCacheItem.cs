using GNF.Cache;

namespace GNF.Distributed.Cache
{
    public interface IDtcCacheItem<out TValue>: ICacheItemOfTValue<TValue>
    {
        string DtcNode { get; }
    }
}
