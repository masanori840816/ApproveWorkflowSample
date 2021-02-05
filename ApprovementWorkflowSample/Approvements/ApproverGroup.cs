using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovementWorkflowSample.Approvements
{
    public record ApproverGroup
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        [Required]
        [Column("workflow_id")]
        [ForeignKey("Workflow")]
        public int WorkflowId { get; init; }
        public Workflow? Workflow { get; init; }
        public List<Approver> Approvers { get; init; } = new List<Approver>();
    }
}