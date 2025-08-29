using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maintenance_Scheduling_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSomeEntitesDontRemember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LCreatedAt",
                table: "TaskLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LCreatedBy",
                table: "TaskLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LCreatedAt",
                table: "TaskLogs");

            migrationBuilder.DropColumn(
                name: "LCreatedBy",
                table: "TaskLogs");
        }
    }
}
