using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GNF.Domain.Entities;

namespace GNF.Simple.Domain.Entities
{
    [Table("WorkOrders", Schema = "dbo")]
    public class WorkOrderEntity : Entity<Guid>
    {
        [Key]
        public override Guid Id { get; set; }
        public string WorkOrderNo { get; set; }
        public int AssistType { get; set; }
        public int Status { get; set; }
        public int Channel { get; set; }
        public string ChannelNumber { get; set; }
        public string ContactNumber { get; set; }
        public string Issue { get; set; }
        public Guid AuthorizeOperatorId { get; set; }
        public string QueryPassword { get; set; }
        public bool IsNeedNotification { get; set; }
        public string ClientCompanyId { get; set; }
        public string ClientCompanyName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
