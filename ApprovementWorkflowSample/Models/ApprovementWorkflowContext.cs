using ApprovementWorkflowSample.Applications;
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
            modelBuilder.Entity<ApplicationUser>()
                .Property(w => w.LastUpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    }
}