using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovementWorkflowSample.Approvements
{
    public class ApproverGroup
    {
        [Key]
        [Column("id")]
        public int Id { get; init; }
        [Required]
        [Column("workflow_id")]
        [ForeignKey("Workflow")]
        public int WorkflowId { get; init; }
        public Workflow? Workflow { get; init; }
        public List<Approver> Approvers { get; init; } = new List<Approver>();
    }
}