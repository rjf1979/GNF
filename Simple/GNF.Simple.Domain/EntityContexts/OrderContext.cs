using GNF.Simple.Infrastructure;

namespace GNF.Simple.Domain.EntityContexts
{
    public class OrderContext:EntityContext
    {
        public OrderContext(string connectionName) : base(connectionName)
        {
        }
    }
}
