using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications;
using ApprovementWorkflowSample.Applications.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Controllers
{
    public class UserController: Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IApplicationUserService users;
        public UserController(ILogger<UserController> logger,
                IApplicationUserService users)
        {
            this.logger = logger;
            this.users = users;
        }
        /*[Route("")]
        public async Task<User?> ShowSignInUserName()
        {
            return await users.GetSignInUserAsync();
        }*/
        [Route("Users/SignIn")]
        public async ValueTask<bool> SignIn(string email, string password)
        {
            if(string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                return false;
            }
            return await users.SignInAsync(email, password);
        }
        [Route("Users/SignOut")]
        public async Task SignOutAsync()
        {
            await users.SignOutAsync();
        }
        [Route("Users/Sample")]
        public async Task<IdentityResult> CreateSampleUser(string userName, string email)
        {
            return await users.CreateAsync(userName, "Hello", email, "test");
        }
    }
}