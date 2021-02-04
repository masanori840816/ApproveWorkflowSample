using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovementWorkflowSample.Approvements
{
    public class ApproverRole
    {
        [Key]
        [Column("id")]
        public int Id { get; init; }
        [Required]
        [Column("name")]
        public string Name { get; init; } = "";
        public List<Approver> Approvers { get; init; } = new List<Approver>();
    }
}