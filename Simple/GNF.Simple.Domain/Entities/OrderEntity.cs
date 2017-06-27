using System;
using GNF.Domain.Entities;

namespace GNF.Simple.Domain.Entities
{
    public class OrderEntity:Entity<Guid>
    {
        public string OrderNo { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal OnlinePayAmount { get; set; }
        public decimal BalancePayAmount { get; set; }
    }
}
