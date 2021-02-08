using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovementWorkflowSample.Approvements
{
    public record Workflow
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        [Required]
        [Column("title")]
        public string Title { get; set; } = "";
        [Required]
        [Column("workflow_type_id")]
        [ForeignKey("WorkflowType")]
        public int WorkflowTypeId { get; set; }
        [Column("approved_date", TypeName = "date")]
        public DateTime? ApprovedDate { get; set; }
        [Required]
        [Column("last_update_date", TypeName = "timestamp with time zone")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdateDate { get; init; }
        public WorkflowType? WorkflowType { get; init; }
        public List<ApproverGroup> ApproverGroups { get; init; } = new List<ApproverGroup>();
    }
}