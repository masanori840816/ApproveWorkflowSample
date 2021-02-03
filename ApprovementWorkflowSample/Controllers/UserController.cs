using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Controllers
{
    public class UserController: Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IApplicationUsers users;
        public UserController(ILogger<UserController> logger,
                IApplicationUsers users)
        {
            this.logger = logger;
            this.users = users;
        }
        [Route("Users/Sample")]
        public async Task<IdentityResult> CreateSampleUser(string userName, string email)
        {
            return await users.CreateAsync(userName, "Hello", email, "test");
        }
    }
}