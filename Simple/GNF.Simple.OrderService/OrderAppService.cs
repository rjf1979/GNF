using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNF.Domain.UnitOfWork;
using GNF.EFUnitwork.UnitOfWork;
using GNF.Simple.Domain.EntityContexts;

namespace GNF.Simple.OrderService
{
    public class OrderAppService
    {
        public void CreateOrder(Domain.Entities.OrderEntity orderEntity)
        {
            using (IUnitOfWork unitOfWork = new EFUnitOfWork(new ConnectionStringResolver("dbConnection")))
            {
                var dbContext = new OrderContext()
            }
        }
    }
}
