namespace GNF.Cache
{
    /// <summary>
    /// 分布式缓存接口
    /// </summary>
    public interface ICache
    {
        long Count { get; }
    }
}
