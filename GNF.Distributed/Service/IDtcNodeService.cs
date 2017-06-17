using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNF.Distributed.Service
{
    /// <summary>
    /// 分布式节点服务
    /// </summary>
    public interface IDtcNodeService
    {
        /// <summary>
        /// 计算分配一个分布式的节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string AllotDtcNode(string key);
        /// <summary>
        /// 注册一个分布式节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        void RegisterDtcNode(string node);
        /// <summary>
        /// 获取所有分布式节点
        /// </summary>
        /// <returns></returns>
        IList<string> GetAllDtcNodes();
    }
}
