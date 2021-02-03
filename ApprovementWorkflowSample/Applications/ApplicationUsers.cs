using System.Threading.Tasks;
using ApprovementWorkflowSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Applications
{
    public class ApplicationUsers: IApplicationUsers
    {
        private readonly ILogger<ApplicationUsers> logger;
        private readonly ApprovementWorkflowContext context;
        public ApplicationUsers(ILogger<ApplicationUsers> logger,
            ApprovementWorkflowContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return null;
            }
            return await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        }
    }
}