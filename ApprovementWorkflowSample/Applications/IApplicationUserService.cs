using System.Threading.Tasks;
using ApprovementWorkflowSample.ActionResults;
using Microsoft.AspNetCore.Identity;

namespace ApprovementWorkflowSample.Applications
{
    public interface IApplicationUserService
    {
        Task<IdentityResult> CreateAsync(string userName, string? organization, string email, string password);
        Task<bool> SignInAsync(string email, string password);
        Task SignOutAsync();
    }
}