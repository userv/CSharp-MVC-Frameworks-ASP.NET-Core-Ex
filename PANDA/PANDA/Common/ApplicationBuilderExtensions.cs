using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using PANDA.Models;

namespace PANDA.Common
{
    public static class ApplicationBuilderExtensions
    {
        private static string DefaultAdminPassword = "admin123";

        private static readonly Dictionary<string, PandaUserRole> roles = new Dictionary<string, PandaUserRole>()
        {
            {"Admin",new PandaUserRole("Admin") },
            {"User",new PandaUserRole("User")}  ,
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<PandaUserRole>>();
                var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<PandaUser>>();
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Key))
                    {
                        await roleManager.CreateAsync(role.Value);
                    }

                }
            }
        }
    }
}