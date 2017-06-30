using System;
using System.Collections.Generic;
using System.Data.Entity;
using GNF.Domain.UnitOfWork;
using GNF.EFUow;
using GNF.Simple.Domain.Entities;
using GNF.Simple.Domain.IRepositoies;
using GNF.Simple.Repositories.Contexts;

namespace GNF.Simple.Repositories
{
    public class WorkOrderRepository : SqlRepository<WorkOrderEntity, Guid>, IWorkOrderRepository
    {
        public WorkOrderRepository(SqlDbContext<WorkOrderEntity> dbContext) : base(dbContext)
        {
        }
    }
}
