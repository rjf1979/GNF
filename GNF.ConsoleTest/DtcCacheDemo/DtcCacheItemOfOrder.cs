using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNF.ConsoleTest.DtcCacheDemo.Entities;
using GNF.Distributed.Cache;

namespace GNF.ConsoleTest.DtcCacheDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class DtcCacheItemOfOrder : IDtcCacheItem<Order>
    {
        public DtcCacheItemOfOrder(string key, Order order, DateTime expireTime)
        {
            //DtcNode = dtcNode;
            Key = key;
            Value = order;
            ExpireTime = expireTime;
        }
        public string DtcNode { get; set; }
        public string Key { get; }
        public Order Value { get; }
        public DateTime ExpireTime { get; }
        public bool IsExpired => ExpireTime < DateTime.Now;
    }
}
