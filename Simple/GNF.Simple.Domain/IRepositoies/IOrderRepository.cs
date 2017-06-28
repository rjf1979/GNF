using System;
using GNF.Simple.Domain.Entities;

namespace GNF.Simple.Domain.IRepositoies
{
    public interface IOrderRepository : GNF.Domain.Repositories.IRepository<OrderEntity, Guid>
    {

    }
}
