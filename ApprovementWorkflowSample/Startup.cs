using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApprovementWorkflowSample.Applications;
using ApprovementWorkflowSample.Models;

namespace ApprovementWorkflowSample
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddDbContext<ApprovementWorkflowContext>(options =>
                options.UseNpgsql(configuration["DbConnection"]));
            
            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddUserStore<ApplicationUserStore>()
                .AddEntityFrameworkStores<ApprovementWorkflowContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IApplicationUsers, ApplicationUsers>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
            });
        }
    }
}
