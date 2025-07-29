
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Maintenance_Scheduling_System.Application.Setting;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Maintenance_Scheduling_System.Infrastructure.Repositories;
using Maintenance_Scheduling_System.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Maintenance_Scheduling_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<ConnectionSettings>(builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<Maintenance_DbContext>();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<Maintenance_DbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<ICurrentUser, FakeCurrentUser>();

            builder.Services.AddScoped<IEquipmentRepo, EquipmentRepository>();
            builder.Services.AddScoped<IMaintenanceHistoryRepo, MaintenanceHistoryRepository>();
            builder.Services.AddScoped<IMainTaskRepo, MainTaskRepository>();

            builder.Services.AddScoped<IEquipmentService,EquipmentService>();
            builder.Services.AddScoped<IMaintenanceHistoryService, MaintenanceHistoryService>();
            builder.Services.AddScoped<IMainTaskService,MainTaskService>();

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Seed(services);
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
