
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Maintenance_Scheduling_System.Application.Services.BackgroundServices;
using Maintenance_Scheduling_System.Application.Setting;
using Maintenance_Scheduling_System.Application.Settings;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Maintenance_Scheduling_System.Infrastructure.Repositories;
using Maintenance_Scheduling_System.Infrastructure.Seeding;
using Maintenance_Scheduling_System.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Maintenance_Scheduling_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                //  Add JWT Bearer Authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer .\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            builder.Services.Configure<TokenSetting>(builder.Configuration.GetSection("JWT"));

            builder.Services.Configure<ConnectionSettings>(builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<Maintenance_DbContext>()
               .AddDefaultTokenProviders();


            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider();
                var jwt = serviceProvider.GetRequiredService<IOptions<TokenSetting>>().Value;

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };

            });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("TechnicianPolicy", policy => policy.RequireRole("Technician"));
            });

            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

           builder.Services.AddHttpContextAccessor();

           builder.Services.AddDbContext<Maintenance_DbContext>();

          
           builder.Services.AddScoped<ICurrentUser, CurrentUserService>();

            builder.Services.AddScoped<IEquipmentRepo, EquipmentRepository>();
            builder.Services.AddScoped<IMaintenanceHistoryRepo, MaintenanceHistoryRepository>();
            builder.Services.AddScoped<IMainTaskRepo, MainTaskRepository>();
            builder.Services.AddScoped<ITaskLogRepo, TaskLogsRepository>();
            builder.Services.AddScoped<ITaskLogAttachmentsRepo, TaskLogAttachmentRepository>();
            builder.Services.AddScoped<IMaintenanceScheduleRepo, MaintenanceScheduleRepository>();
            builder.Services.AddScoped<IScheduleTaskRepo, ScheduleTaskRepository>();
            builder.Services.AddScoped<IAppUserRepo, AppUserRepository>();
            builder.Services.AddScoped<IRefreshTokenRepo, RefreshTokenRepository>();

            builder.Services.AddScoped<IEquipmentService,EquipmentService>();
            builder.Services.AddScoped<IMaintenanceHistoryService, MaintenanceHistoryService>();
            builder.Services.AddScoped<IMainTaskService,MainTaskService>();
            builder.Services.AddScoped<IMainTaskLogService,MainTaskLogService>();
            builder.Services.AddScoped<ITaskLogAttachmentService,TaskLogAttachmentService>();
            builder.Services.AddScoped<IMaintenanceScheduleService,MaintenanceScheduleService>();
            builder.Services.AddScoped<IScheduleTaskService,ScheduleTaskService>();
            builder.Services.AddScoped<IAppUserService,AppUserService>();
            builder.Services.AddScoped<IRefreshTokenService,RefreshTokenService>();

            builder.Services.AddHostedService<TaskBackgroundService>();
            builder.Services.AddHostedService<ScheduleBackgroundService>();

            
            var app = builder.Build();


            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                SeedData data = new();
                data.Seed(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding failed: {ex.Message}");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
