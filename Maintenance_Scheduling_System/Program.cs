
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Maintenance_Scheduling_System.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;

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

            builder.Services.AddDbContext<Maintenance_DbContext>();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<Maintenance_DbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<WeatherForecast>();

            var app = builder.Build();

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
