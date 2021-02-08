using System;
using System.Collections.Generic;

namespace ApprovementWorkflowSample.Approvements
{
    public record DisplayWorkflow(int Id, string Title, WorkflowType? WorkflowType, 
        DateTime? ApprovedDate, List<DisplayApproverGroup> ApproverGroups);
    public record DisplayApproverGroup(int Id, List<DisplayApprover> Approvers);
    public record DisplayApprover(int Id, string Name, ApproverRole Role, DateTime? ApprovedDate,
        bool NextApprover, bool SignInUser);
}