using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovementWorkflowSample.Approvements
{
    public class Approver
    {
        [Key]
        [Column("id")]
        public int Id { get; init; }
        [Required]
        [Column("name")]
        public string Name { get; init; } = "";
        [Required]
        [Column("approver_group_id")]
        [ForeignKey("ApproverGroup")]
        public int ApproverGroupId { get; init; }
        [Required]
        [Column("approver_role_id")]
        [ForeignKey("ApproverRole")]
        public int ApproverRoleId { get; init; }
        [Required]
        [Column("last_update_date", TypeName = "timestamp with time zone")]
        public DateTime LastUpdateDate { get; init; }

        public ApproverGroup? ApproverGroup { get; init; }
        public ApproverRole? ApproverRole { get; init; }
    }
}