using System;
using GNF.Domain.UnitOfWork;
using GNF.Simple.Domain.IRepositoies;
using SqlSugar;

namespace GNF.Simple.Repositories
{
    public class OrderRepository:SqlSugarUnitOfWork.SqlRepository<Domain.Entities.OrderEntity,Guid>,IOrderRepository
    {
        public OrderRepository(IDbContext<SqlSugarClient> dbContext) : base(dbContext)
        {
        }
    }
}
