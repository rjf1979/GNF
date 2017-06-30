
using System;
using GNF.Common.Encrypt;

namespace GNF.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = RSA.GetRasKey();
            var str = "abc";
            var estr = RSA.EncryptString(str, key.PublicKey);//加密
            var dest = RSA.DecryptString(estr, key.PrivateKey);//解密
            Console.WriteLine(dest);
            Console.Read();
            //Order order = new Order
            //{
            //    OrderId = Guid.NewGuid(),
            //    OrderNo = $"OD{DateTime.Now.ToString("yyMMddHHmmss")}"
            //};
            //Console.WriteLine($"创建的订单号：{order.OrderNo}");
            //DtcCacheItemOfOrder dtcCacheItemOfOrder = new DtcCacheItemOfOrder(order.OrderId.ToString(), order, DateTime.Now.AddDays(1));//定义一个缓存实体对象
            //var redisCache = new RedisCache();//创建redis缓存操作实现对象
            //Task.Run(async () =>
            //{
            //    var result = await redisCache.SaveAsync(dtcCacheItemOfOrder);//异步保存缓存对象
            //    Console.WriteLine($"执行成功：{result}");
            //}).Wait();
            ////获取缓存对象
            //var cacheItem = redisCache.GetAsync(order.OrderId.ToString()).Result;
            //Console.WriteLine($"查到的订单号：{cacheItem.Value.OrderNo}");
            //Console.Read();
        }
    }
}
