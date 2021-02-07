using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApprovementWorkflowSample.Applications;
using ApprovementWorkflowSample.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Server;

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
            services.AddHttpClient();
            
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddDbContext<ApprovementWorkflowContext>(options =>
                options.UseNpgsql(configuration["DbConnection"]));
            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddUserStore<ApplicationUserStore>()
                .AddEntityFrameworkStores<ApprovementWorkflowContext>()
                .AddDefaultTokenProviders();
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                //    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    options.LoginPath = new PathString("/Pages/SignIn");
                });
            services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp =>
                (ServerAuthenticationStateProvider) sp.GetRequiredService<AuthenticationStateProvider>()
            );
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
