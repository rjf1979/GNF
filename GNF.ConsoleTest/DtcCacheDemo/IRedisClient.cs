using System.Collections;

namespace GNF.ConsoleTest.DtcCacheDemo
{
    public interface IRedisClient
    {
        Hashtable Contains { get; }
    }
}