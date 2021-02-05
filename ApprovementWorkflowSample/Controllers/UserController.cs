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
        [HttpPost]
        [Route("Users/SignIn")]
        public async ValueTask<bool> SignIn([FromBody]SignInValue value)
        {
            if(string.IsNullOrEmpty(value.Email) ||
                string.IsNullOrEmpty(value.Password))
            {
                return false;
            }
            return await users.SignInAsync(value.Email, value.Password);
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