﻿using System.Collections.Generic;
using System.Linq;

namespace GNF.Common.Arithmetic
{
    /// <summary>
    /// 一致性哈希算法
    /// </summary>
    public class ConsistentHashing
    {
        //原文中的JAVA类TreeMap实现了Comparator方法，这里我图省事，直接用了net下的SortedList，其中Comparer接口方法）
        private readonly SortedList<long, string> _ketamaNodes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="nodeCopies">需要的虚拟节点数量</param>
        public ConsistentHashing(IList<string> nodes, int nodeCopies = 160)
        {
            _ketamaNodes = new SortedList<long, string>();
            var numReps = nodeCopies;
            //对所有节点，生成nCopies个虚拟结点
            foreach (string node in nodes)
            {
                //每四个虚拟结点为一组
                for (int i = 0; i < numReps / 4; i++)
                {
                    //getKeyForNode方法为这组虚拟结点得到惟一名称 
                    byte[] digest = HashAlgorithm.ComputeMd5(node + i);
                    /** Md5是一个16字节长度的数组，将16字节的数组每四个字节一组，分别对应一个虚拟结点，这就是为什么上面把虚拟结点四个划分一组的原因*/
                    for (int h = 0; h < 4; h++)
                    {
                        long m = HashAlgorithm.Hash(digest, h);
                        _ketamaNodes[m] = node;
                    }
                }
            }
        }

        public string GetPrimary(string k)
        {
            byte[] digest = HashAlgorithm.ComputeMd5(k);
            string rv = GetNodeForKey(HashAlgorithm.Hash(digest, 0));
            return rv;
        }

        string GetNodeForKey(long hash)
        {
            long key = hash;
            //如果找到这个节点，直接取节点，返回   
            if (!_ketamaNodes.ContainsKey(key))
            {
                //得到大于当前key的那个子Map，然后从中取出第一个key，就是大于且离它最近的那个key 说明详见: http://www.javaeye.com/topic/684087
                var tailMap = (from coll in _ketamaNodes
                               where coll.Key > hash
                               select new { coll.Key }).ToList();
                if (!tailMap.Any())
                    key = _ketamaNodes.FirstOrDefault().Key;
                else
                {
                    var firstOrDefault = tailMap.FirstOrDefault();
                    if (firstOrDefault != null) key = firstOrDefault.Key;
                }
            }
            var rv = _ketamaNodes[key];
            return rv;
        }
    }
}
