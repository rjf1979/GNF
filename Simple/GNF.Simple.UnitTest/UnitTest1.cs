using System;
using System.Linq;
using GNF.Domain.UnitOfWork;
using GNF.EFUow;
using GNF.Simple.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GNF.Simple.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetWorkOrders()
        {
            IConnectionStringResolver connectionStringResolver = new ConnectionStringResolver("Asmkt.WorkOrderSystem.Database");
            using (var uow = new EfUnitOfWork(connectionStringResolver))
            {
                var context = new EfDbContext<WorkOrderEntity, Guid>(uow.DbContext);
                var datas = context.Entities.ToList();
                Console.WriteLine(context.CurrentContext.Database.Connection.GetHashCode());
                Assert.IsTrue(datas != null);
                var repository2 = new EfDbContext<WorkOrderFlowEntity, Guid>(uow.DbContext);
                var entity = repository2.Entities.Find(Guid.Parse("07B60E03-1BFC-4C5C-9109-13CC6F77375B"));
                Console.WriteLine(repository2.CurrentContext.Database.Connection.GetHashCode());
                Assert.IsTrue(entity != null);

                uow.Begin();
            }
        }
    }
}
