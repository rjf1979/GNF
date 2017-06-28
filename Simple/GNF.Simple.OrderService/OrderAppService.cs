using System;
using GNF.Domain.UnitOfWork;
using GNF.Simple.Domain.Entities;
using GNF.Simple.Domain.IRepositoies;
using GNF.Simple.Repositories;
using GNF.SqlSugarUnitOfWork;

namespace GNF.Simple.OrderService
{
    public class OrderAppService
    {
        public async void CreateOrder(OrderEntity orderEntity)
        {
            IConnectionStringResolver connectionStringResolver = new ConnectionStringResolver("Name");
            using (var unitOfWork = new SqlUnitOfWork(connectionStringResolver))
            {
                OrderEntity order = new OrderEntity();
                IOrderRepository orderRepository = new OrderRepository(unitOfWork.DbContext);
                unitOfWork.Begin();
                var result = await orderRepository.InsertAsync(order);
                if (!result)
                {
                    unitOfWork.RollBack();
                    return;
                }
                unitOfWork.Complete();
            }
        }

        public OrderEntity GetOrder(Guid orderId)
        {
            IConnectionStringResolver connectionStringResolver = new ConnectionStringResolver("Name");
            IDbContextResolver dbContextResolver = new DbContextResolver();
            using (var dbContext = dbContextResolver.Resolve<SqlDbContext>(connectionStringResolver))
            {
                IOrderRepository orderRepository = new OrderRepository(dbContext);
                return orderRepository.Get(orderId);
            }
        }
    }
}
