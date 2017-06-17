using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GNF.Common.Arithmetic;
using GNF.Distributed.Cache;

namespace GNF.ConsoleTest.DtcCacheDemo
{
    public class RedisCache : IDtcCacheAsync<DtcCacheItemOfOrder>
    {
        private readonly ConsistentHashing _consistentHashing;

        public RedisCache()
        {
            _consistentHashing = new ConsistentHashing(RedisContains.Servers);//一致哈希算法
        }

        public long Count { get; }

        public async Task<bool> SaveAsync(DtcCacheItemOfOrder cacheItem)
        {
            return await Task.Run(() =>
            {
                var currentNode = _consistentHashing.CalculatedNode(cacheItem.Key);//计算分配到哪个服务节点上
                IRedisClient redisClient = RedisContains.Connect(currentNode);//查找一个对应服务节点的redis客户端
                cacheItem.DtcNode = currentNode;
#if DEBUG
                Console.WriteLine($"保存节点：{currentNode}");
#endif
                if (redisClient.Contains.ContainsKey(cacheItem.DtcNode))
                {
                    redisClient.Contains[cacheItem.Key] = cacheItem;
                }
                else
                {
                    redisClient.Contains.Add(cacheItem.Key, cacheItem);
                }
                return true;
            });
        }

        public Task<bool> SaveAsync(IList<DtcCacheItemOfOrder> cacheItems)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DtcCacheItemOfOrder> GetAsync(string key)
        {
            return await Task.Run(() =>
            {
                var currentNode = _consistentHashing.CalculatedNode(key);//计算分配到哪个服务节点上
#if DEBUG
                Console.WriteLine($"查询节点：{currentNode}");
#endif
                IRedisClient redisClient = RedisContains.Connect(currentNode);//查找一个对应服务节点的redis客户端
                return (DtcCacheItemOfOrder)redisClient.Contains[key];
            });

        }

        public Task<IList<DtcCacheItemOfOrder>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<TValue>> GetAllValuesAsync<TValue>()
        {
            throw new System.NotImplementedException();
        }
    }
}
