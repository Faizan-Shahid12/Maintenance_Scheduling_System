using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maintenance_Scheduling_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAttributesofScheduleTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Interval",
                table: "ScheduleTasks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "TechnicianId",
                table: "ScheduleTasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "ScheduleTasks");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "ScheduleTasks");
        }
    }
}
