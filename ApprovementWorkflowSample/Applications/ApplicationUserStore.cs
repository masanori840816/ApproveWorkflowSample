using System;
using System.Threading;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Applications
{
    public class ApplicationUserStore: IUserPasswordStore<ApplicationUser>
    {
        private readonly ILogger<ApplicationUserStore> logger;
        private readonly ApprovementWorkflowContext context;

        public ApplicationUserStore(ILogger<ApplicationUserStore> logger,
            ApprovementWorkflowContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            string validationError = user.Validate();
            if(string.IsNullOrEmpty(validationError) == false)
            {
                return IdentityResult.Failed(new IdentityError { Description = validationError });
            }
            using(IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                if(await context.ApplicationUsers
                    .AnyAsync(u => u.Email == user.Email,
                    cancellationToken))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Your e-mail address is already used" });
                }
                var newUser = new ApplicationUser();
                newUser.Update(user);
                await context.ApplicationUsers.AddAsync(newUser, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            ApplicationUser? target = await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == user.Id,
                cancellationToken);
            if (target == null)
            {
                return IdentityResult.Success;
            } 
            context.ApplicationUsers.Remove(target);
            await context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // do nothing
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return new ApplicationUser();
            }
            if(int.TryParse(userId, out var id) == false)
            {
                return new ApplicationUser();
            }
            return await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == id,
                cancellationToken);
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(normalizedUserName))
            {
                return new ApplicationUser();
            }
            return await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName.ToUpper() == normalizedUserName,
                cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedUserName);
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            // always true
            return await Task.FromResult(true);
        }

        public async Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            // do nothing
            await Task.Run(() => {});
        }

        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            using(IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                ApplicationUser? target = await context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == user.Id,
                    cancellationToken);
                if (target == null)
                {
                    logger.LogError("[SetPasswordHashAsync] Target was not found");
                    return;
                }
                target.PasswordHash = passwordHash;
                string validationError = target.Validate();
                if(string.IsNullOrEmpty(validationError) == false)
                {
                    logger.LogError($"[SetPasswordHashAsync] {validationError}");
                    await transaction.RollbackAsync();
                    return;
                }
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }            
        }

        public async Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            using(IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                ApplicationUser? target = await context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == user.Id,
                    cancellationToken);
                if (target == null)
                {
                    logger.LogError("[SetUserNameAsync] Target was not found");
                    return;
                }
                target.UserName = userName;
                string validationError = target.Validate();
                if(string.IsNullOrEmpty(validationError) == false)
                {
                    logger.LogError($"[SetUserNameAsync] {validationError}");
                    await transaction.RollbackAsync();
                    return;
                }
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            } 
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            string validationError = user.Validate();
            if(string.IsNullOrEmpty(validationError) == false)
            {
                return IdentityResult.Failed(new IdentityError { Description = validationError });
            }
            using(IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                ApplicationUser? target = await context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == user.Id,
                    cancellationToken);
                if (target == null)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Target was not found" });
                }
                if(await context.ApplicationUsers
                    .AnyAsync(u => u.Id != user.Id && u.Email == user.Email,
                    cancellationToken))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Your e-mail address is already used" });
                }
                target.Update(user);
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
                return IdentityResult.Success;
            }
        }
    }
}