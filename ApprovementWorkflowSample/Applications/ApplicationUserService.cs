using System;
using System.Threading.Tasks;
using ApprovementWorkflowSample.ActionResults;
using ApprovementWorkflowSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Applications
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly ILogger<ApplicationUsers> logger;
        private readonly ApprovementWorkflowContext context;
        private readonly IApplicationUsers applicationUsers;

        public ApplicationUserService(ILogger<ApplicationUsers> logger,
            ApprovementWorkflowContext context,
            IApplicationUsers applicationUsers)
        {
            this.logger = logger;
            this.context = context;
            this.applicationUsers = applicationUsers;

        }
        public async Task<IdentityResult> CreateAsync(string userName,
                string? organization, string email, string password)
        {
            return await applicationUsers.CreateAsync(userName, organization, email, password);
        }
        public async Task<bool> SignInAsync(string email, string password)
        {
            var target = await applicationUsers.GetByEmailAsync(email);
            if (target == null)
            {
                return false;
            }
            return await applicationUsers.SignInAsync(target, password);
        }
        public async Task SignOutAsync()
        {
            await applicationUsers.SignOutAsync();
        }
        
    }
}