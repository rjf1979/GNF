using System;
using GNF.Domain.UnitOfWork;
using GNF.EFUnitOfWork;
using GNF.Simple.Domain.Entities;
using GNF.Simple.Domain.IRepositoies;

namespace GNF.Simple.Repositories
{
    public class OrderRepository:SqlRepository<OrderEntity,Guid>,IOrderRepository
    {
        public OrderRepository(IDbContext<SqlSugarClient> dbContext) : base(dbContext)
        {
        }
    }
}
