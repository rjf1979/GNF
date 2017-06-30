using System;
using GNF.Domain.Repositories;
using GNF.Simple.Domain.Entities;

namespace GNF.Simple.Domain.IRepositoies
{
    public interface IWorkOrderRepository: IRepository<WorkOrderEntity, Guid>
    {
    }
}
