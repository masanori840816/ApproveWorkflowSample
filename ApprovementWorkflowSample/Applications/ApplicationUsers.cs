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
        private readonly SignInManager<ApplicationUser> signInManager;
        public ApplicationUsers(ILogger<ApplicationUsers> logger,
            ApprovementWorkflowContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            this.logger = logger;
            this.context = context;
            this.signInManager = signInManager;
        }
        public async Task<bool> SignInAsync(ApplicationUser user, string password)
        {
            var result = await signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> CreateAsync(string userName, string? organization, string email, string password)
        {
            var newUser = new ApplicationUser();
            newUser.Update(userName, organization, email, password);
            string validationError = newUser.Validate();
            if (string.IsNullOrEmpty(validationError) == false)
            {
                return IdentityResult.Failed(new IdentityError { Description = validationError });
            }
            return await signInManager.UserManager.CreateAsync(newUser);
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
        public async Task<bool> CheckDuplicateEmailUserExistsAsync(string email)
        {
            return await context.ApplicationUsers
                    .AnyAsync(u => u.Email.ToUpper() == email.ToUpper());
        }
        public async Task<bool> CheckDuplicateEmailUserExistsAsync(int userId, string email)
        {
            return await context.ApplicationUsers
                    .AnyAsync(u => u.Id != userId && u.Email.ToUpper() == email.ToUpper());
        }
    }
}