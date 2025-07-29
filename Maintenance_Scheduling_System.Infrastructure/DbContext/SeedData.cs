using Maintenance_Scheduling_System.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.DbContext
{
    public static class SeedData
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Maintenance_DbContext>();

            if (!context.WorkShopLocs.Any())
            {
                context.WorkShopLocs.AddRange(
                    new WorkShopLoc { Name = "Central Repair Hub", Location = "123 Industrial Zone, Karachi" },
                    new WorkShopLoc { Name = "Northside Maintenance", Location = "88 GT Road, Lahore" },
                    new WorkShopLoc { Name = "RapidFix Engineering", Location = "45 Service Road East, Islamabad" },
                    new WorkShopLoc { Name = "HeavyTech Services", Location = "Plot 19, Sector I-10, Islamabad" },
                    new WorkShopLoc { Name = "Metro Mechanics", Location = "201 Saddar Town, Rawalpindi" },
                    new WorkShopLoc { Name = "Precision Tools & Repair", Location = "67 Model Town Extension, Lahore" },
                    new WorkShopLoc { Name = "EquipMaster Facility", Location = "Building 5, Blue Area, Islamabad" },
                    new WorkShopLoc { Name = "PowerPlant Workshop", Location = "Sector G-8, Islamabad" },
                    new WorkShopLoc { Name = "SafeHands Repairs", Location = "University Road, Peshawar" },
                    new WorkShopLoc { Name = "Southside Machinery Works", Location = "Phase 2, DHA, Karachi" }
                );
                
            }
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string [] roles = { "Admin", "Techinican" };

            foreach (var role in roles)
            {
                if(!roleManager.RoleExistsAsync(role).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            context.SaveChanges();
        }
    }

}
