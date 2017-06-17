using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GNF.ConsoleTest.DtcCacheDemo
{
    public class RedisClient : IRedisClient
    {
        public Hashtable Contains { get; } = new Hashtable();
    }

    public class RedisContains
    {
        static readonly IDictionary<string, RedisClient> _dictionary = new Dictionary<string, RedisClient>();

        static RedisContains()
        {
            _dictionary.Add("192.168.110.101", new RedisClient());
            _dictionary.Add("192.168.110.102", new RedisClient());
            _dictionary.Add("192.168.110.103", new RedisClient());
            _dictionary.Add("192.168.110.104", new RedisClient());
        }

        public static IList<string> Servers => _dictionary.Keys.ToList();

        public static IRedisClient Connect(string server)
        {
            if (_dictionary.ContainsKey(server))
            {
                return _dictionary[server];
            }
            throw new KeyNotFoundException(nameof(server));
        }
    }
}
