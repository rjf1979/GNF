using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNF.Simple.Domain.Entities;

namespace GNF.Simple.Domain.IRepositoies
{
    public interface IOrderRepository : GNF.Domain.Repositories.IRepository<OrderEntity, Guid>
    {

    }
}
