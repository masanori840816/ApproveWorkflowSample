using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovementWorkflowSample.Approvements
{
    public class Workflow
    {
        [Key]
        [Column("id")]
        public int Id { get; init; }
        [Required]
        [Column("title")]
        public string Title { get; init; } = "";
        [Required]
        [Column("workflow_type_id")]
        [ForeignKey("WorkflowType")]
        public int WorkflowTypeId { get; init; }
        [Required]
        [Column("last_update_date", TypeName = "timestamp with time zone")]
        public DateTime LastUpdateDate { get; init; }
        public WorkflowType? WorkflowType { get; init; }
        public List<ApproverGroup> ApproverGroups { get; init; } = new List<ApproverGroup>();
    }
}