using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maintenance_Scheduling_System.Migrations
{
    /// <inheritdoc />
    public partial class FixedBugofIntervalofScheduleTask2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Interval",
                table: "ScheduleTasks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "ScheduleTasks");
        }
    }
}
