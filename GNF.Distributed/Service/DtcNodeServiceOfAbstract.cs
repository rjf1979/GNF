using System;
using System.Collections.Generic;
using GNF.Common.Arithmetic;

namespace GNF.Distributed.Service
{
    public abstract class DtcNodeServiceOfAbstract : IDtcNodeService
    {
        private readonly ConsistentHashing _consistentHashing;
        protected DtcNodeServiceOfAbstract()
        {
            _consistentHashing = new ConsistentHashing();
        }

        public virtual string AllotDtcNode(string key)
        {
            return _consistentHashing.CalculatedNode(key);
        }

        public void RegisterDtcNode(string node)
        {
            _consistentHashing.AddNode(node);
        }

        public IList<string> GetAllDtcNodes()
        {
            throw new NotImplementedException();
        }
    }
}
