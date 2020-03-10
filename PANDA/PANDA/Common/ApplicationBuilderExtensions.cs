using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PANDA.Data;
using PANDA.Models;

namespace PANDA.Common
{
    public static class ApplicationBuilderExtensions
    {
        private static string DefaultAdminPassword = "admin123";

        private static readonly Dictionary<string, PandaUserRole> Roles = new Dictionary<string, PandaUserRole>()
        {
            {"Admin",new PandaUserRole("Admin") },
            {"User",new PandaUserRole("User")}  ,
        };

        private static readonly string[] Statuses = { "Pending", "Shipped", "Delivered", "Acquired" };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<PandaUserRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<PandaUser>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<PandaDbContext>();
                foreach (var role in Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Key))
                    {
                        await roleManager.CreateAsync(role.Value);
                    }

                }
                if (dbContext.PackageStatuses.Any())
                {
                    return;
                }
                foreach (var status in Statuses)
                {
                    await dbContext.PackageStatuses.AddAsync(new PackageStatus() {Name = status});
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}