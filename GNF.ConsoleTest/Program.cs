﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNF.ConsoleTest.DtcCacheDemo;
using GNF.ConsoleTest.DtcCacheDemo.Entities;

namespace GNF.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
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

            while (true)
            {
                Task.Run(() =>
                {
                    var n = Inc();
                    _dictionary.Add(n, n);
                });
            }
        }

        static readonly IDictionary<int, int> _dictionary = new Dictionary<int, int>();
        private static int i = 0;

        static int Inc()
        {
            i++;
            return i;
        }
    }
}
