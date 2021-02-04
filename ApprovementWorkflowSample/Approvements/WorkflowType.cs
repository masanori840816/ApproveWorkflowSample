using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovementWorkflowSample.Approvements
{
    public class WorkflowType
    {
        [Key]
        [Column("id")]
        public int Id { get; init; }
        [Required]
        [Column("Name")]
        public string Name { get; init; } = "";
        public List<Workflow>? Workflows { get; init; }
    }
}