using System.Data.Entity;
using GNF.Simple.Domain.Entities;

namespace GNF.Simple.Repositories.Contexts
{
    public class WorkOrderContext:DbContext
    {
        public WorkOrderContext(string connnectionName) : base(connnectionName)
        {
            
        }

        public DbSet<WorkOrderEntity> WorkOrders { get; set; }
    }
}
