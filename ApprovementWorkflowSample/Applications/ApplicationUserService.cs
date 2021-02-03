using System.Security.Claims;
using System;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications.Dto;
using ApprovementWorkflowSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Applications
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly ILogger<ApplicationUsers> logger;
        private readonly ApprovementWorkflowContext context;
        private readonly IApplicationUsers applicationUsers;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApplicationUserService(ILogger<ApplicationUsers> logger,
            ApprovementWorkflowContext context,
            IApplicationUsers applicationUsers,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.context = context;
            this.applicationUsers = applicationUsers;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
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
        public async ValueTask<User?> GetSignInUserAsync()
        {
            ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            if(signInManager.IsSignedIn(user) == false)
            {
                return null;
            }
            string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId) ||
                int.TryParse(userId, out var id) == false)
            {
                return null;
            }
            ApplicationUser? appUser = await applicationUsers.GetByIdAsync(id);
            if (appUser == null)
            {
                return null;
            }
            return new User(appUser.Id, appUser.UserName, appUser.Organization, appUser.Email);
        }
        public async Task<bool> SignInAsync(string email, string password)
        {
            var target = await applicationUsers.GetByEmailAsync(email);
            if (target == null)
            {
                return false;
            }
            var result = await signInManager.PasswordSignInAsync(target, password, false, false);
            return result.Succeeded;
        }
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
        
    }
}