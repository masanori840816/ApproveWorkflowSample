using ApprovementWorkflowSample.Applications;
using ApprovementWorkflowSample.Approvements;
using Microsoft.EntityFrameworkCore;

namespace ApprovementWorkflowSample.Models
{
    public class ApprovementWorkflowContext: DbContext
    {
        public ApprovementWorkflowContext(DbContextOptions<ApprovementWorkflowContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ApplicationUser
            var applicationUserEntity = modelBuilder.Entity<ApplicationUser>();
            applicationUserEntity.Property(w => w.LastUpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            applicationUserEntity
                .HasIndex(u => u.Email)
                .IsUnique();
            // Approver
            var approverEntity = modelBuilder.Entity<Approver>();
            approverEntity.Property(a => a.LastUpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            approverEntity.HasOne(a => a.ApproverGroup)
                .WithMany(g => g!.Approvers)
                .HasForeignKey(a => a.ApproverGroupId);
            approverEntity.HasOne(a => a.ApproverRole)
                .WithMany(r => r!.Approvers)
                .HasForeignKey(a => a.ApproverRoleId);
            // ApproverGroup
            var approverGroupEntity = modelBuilder.Entity<ApproverGroup>();
            approverGroupEntity.HasOne(g => g.Workflow)
                .WithMany(w => w!.ApproverGroups)
                .HasForeignKey(g => g.WorkflowId);
            // Workflow
            var workflowEntity = modelBuilder.Entity<Workflow>();
            workflowEntity.Property(a => a.LastUpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            workflowEntity.HasOne(w => w.WorkflowType)
                .WithMany(t => t!.Workflows)
                .HasForeignKey(w => w.WorkflowTypeId);
            // ApproverRole
            var approverRoleEntity = modelBuilder.Entity<ApproverRole>();
            approverRoleEntity.HasData(
                new ApproverRole
                {
                    Id = 1,
                    Name = "Author",
                },
                new ApproverRole
                {
                    Id = 2,
                    Name = "Checker",
                },
                new ApproverRole
                {
                    Id = 3,
                    Name = "Approver",
                }
            );
            // WorkflowType
            var workflowTypeEntity = modelBuilder.Entity<WorkflowType>();
            workflowTypeEntity.HasData(
                new WorkflowType
                {
                    Id = 1,
                    Name = "WorkflowTypeA",
                },
                new WorkflowType
                {
                    Id = 2,
                    Name = "WorkflowTypeA",
                }
            );
        }
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    }
}