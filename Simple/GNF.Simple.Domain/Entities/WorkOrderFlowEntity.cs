using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GNF.Domain.Entities;

namespace GNF.Simple.Domain.Entities
{
    [Table("WorkOrderFlows",Schema = "dbo")]
    public class WorkOrderFlowEntity:Entity<Guid>
    {
        [Key]
        public override Guid Id { get; set; }
        public Guid WorkOrderId { get; set; }
        public Guid ProcessorId { get; set; }
    }
}
