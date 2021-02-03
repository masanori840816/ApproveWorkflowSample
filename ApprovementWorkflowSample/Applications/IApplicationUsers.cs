using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApprovementWorkflowSample.Applications
{
    public interface IApplicationUsers
    {
        Task<bool> SignInAsync(ApplicationUser user, string password);
        Task SignOutAsync();
        Task<IdentityResult> CreateAsync(string userName, string? organization, string email, string password);
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<bool> CheckDuplicateEmailUserExistsAsync(string email);
        Task<bool> CheckDuplicateEmailUserExistsAsync(int userId, string email);
    }
}