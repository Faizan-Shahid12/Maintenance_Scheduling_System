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
using System.Reflection.Emit;
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
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("Maintenance_Scheduling_System.Infrastructure"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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

                entity.Property(s => s.Interval)
                    .HasConversion(interval => interval.TotalDays, interval => TimeSpan.FromDays(interval))
                    .HasColumnType("float");

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

            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.TokenId);

                entity.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<MaintenanceHistory> MaintenanceHistories { get; set; }
        public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public DbSet<ScheduleTask> ScheduleTasks { get; set; }
        public DbSet<MainTask> MainTask { get; set; }
        public DbSet<TaskLogs> TaskLogs { get; set; }
        public DbSet<TaskLogAttachment> TaskLogsAttachments { get; set; }
        public DbSet<WorkShopLoc> WorkShopLocs { get; set; }

    }
}
