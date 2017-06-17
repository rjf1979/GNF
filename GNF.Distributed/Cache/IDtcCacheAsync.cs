using GNF.Cache;

namespace GNF.Distributed.Cache
{
    public interface IDtcCacheAsync<TCacheItem> : ICacheAsync<TCacheItem>, IDtcCache where TCacheItem : ICacheItem
    {
    }
}
