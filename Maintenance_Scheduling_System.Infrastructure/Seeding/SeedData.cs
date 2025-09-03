using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Seeding
{
    public class SeedData
    {
        public async Task Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Maintenance_DbContext>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string [] roles = { "Admin", "Technician" };

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            await context.SaveChangesAsync();
        }
    }

}
