using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApprovementWorkflowSample.Applications
{
    public interface IApplicationUsers
    {
        Task<ApplicationUser?> GetByIdAsync(int id);
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}