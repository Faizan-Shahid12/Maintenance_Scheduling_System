using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.Enums;
using Maintenance_Scheduling_System.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.DbContext
{
    public class Maintenance_DbContext : IdentityDbContext<AppUser>
    {
        public string ConnectionString { get; set; }

        public Maintenance_DbContext(DbContextOptions<Maintenance_DbContext> options, IOptions<ConnectionSettings> options1) : base(options)
        {
                  ConnectionString = options1.Value.DefaultString;
        }
        public Maintenance_DbContext()
    : base(new DbContextOptionsBuilder<Maintenance_DbContext>()
        .UseSqlServer("Server=localhost;Database=MaintenanceDb;Trusted_Connection=True;TrustServerCertificate=True;",
            b => b.MigrationsAssembly("Maintenance_Scheduling_System.Infrastructure"))
        .Options)
        {
            ConnectionString = "Server=localhost;Database=MaintenanceDb;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("Maintenance_Scheduling_System"));
        }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                   new IdentityRole() { Id = "A1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                   new IdentityRole() { Id = "T1", Name = "Technician", ConcurrencyStamp = "2", NormalizedName = "TECHNICIAN" }
                );
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRole(builder);

            builder.Entity<AppUser>(entity =>
            {

                entity.HasKey(x => x.Id);

                entity.HasMany(x => x.AssignedTasks)
                .WithOne(t => t.Technician)
                .HasForeignKey(t => t.TechnicianId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            });

            builder.Entity<Equipment>(entity =>
            {
                entity.HasKey(x => x.EquipmentId);

                entity.HasOne(e => e.WorkShopLocation)
                .WithMany(w => w.equipments)
                .IsRequired(false)
                .HasForeignKey(e => e.WorkshopId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.Schedule)
                .WithOne(s => s.equipment)
                .HasForeignKey(s => s.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.History)
                .WithOne(h => h.equipment)
                .HasForeignKey(h => h.EquipmentId);

                entity.HasMany(e => e.Tasks)
                .WithOne(t => t.Equipment)
                .IsRequired(false)
                .HasForeignKey(t => t.EquipmentId);

            });

            builder.Entity<MaintenanceSchedule>(entity =>
            {
                entity.HasKey(s => s.ScheduleId);

                entity.HasMany(s => s.ScheduleTasks)
                .WithOne(st => st.schedule)
                .HasForeignKey(st => st.ScheduleId)
                .IsRequired();
                
            });

            builder.Entity<MaintenanceHistory>(entity =>
            {
                entity.HasKey(h => h.HistoryId);

                entity.HasMany(h => h.tasks)
                .WithOne(t => t.History)
                .HasForeignKey(t => t.HistoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
            });
            builder.Entity<MainTask>(entity =>
            {
                entity.HasKey(mt => mt.TaskId);

                entity.HasMany(mt => mt.Logs)
                .WithOne(tl => tl.task)
                .HasForeignKey(tl => tl.TaskId)
                .IsRequired(false);

            });
            builder.Entity<TaskLogs>(entity =>
            {
                entity.HasKey(tl => tl.LogId);

                entity.HasMany(tl => tl.Attachments)
                .WithOne(tla => tla.TaskLog)
                .HasForeignKey(tla => tla.LogId)
                .IsRequired(false);
            });
            builder.Entity<WorkShopLoc>(entity =>
            {
                entity.HasKey(w => w.WorkShopId);
            });
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<MaintenanceHistory> MaintenanceHistories { get; set; }
        public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public DbSet<MainTask> Tasks { get; set; }
        public DbSet<TaskLogs> TaskLogs { get; set; }
        public DbSet<TaskLogAttachment> TaskLogsAttachments { get; set; }
        public DbSet<WorkShopLoc> workShopLocs { get; set; }

    }
}
