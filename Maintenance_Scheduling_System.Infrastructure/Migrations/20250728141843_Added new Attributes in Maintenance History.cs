using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Maintenance_Scheduling_System.Migrations
{
    /// <inheritdoc />
    public partial class AddednewAttributesinMaintenanceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_workShopLocs_WorkshopId",
                table: "Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workShopLocs",
                table: "workShopLocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "T1");

            migrationBuilder.RenameTable(
                name: "workShopLocs",
                newName: "WorkShopLocs");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "MaintenanceHistories",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "MaintenanceHistories",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkShopLocs",
                table: "WorkShopLocs",
                column: "WorkShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_WorkShopLocs_WorkshopId",
                table: "Equipment",
                column: "WorkshopId",
                principalTable: "WorkShopLocs",
                principalColumn: "WorkShopId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_WorkShopLocs_WorkshopId",
                table: "Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkShopLocs",
                table: "WorkShopLocs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "MaintenanceHistories");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "MaintenanceHistories");

            migrationBuilder.RenameTable(
                name: "WorkShopLocs",
                newName: "workShopLocs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workShopLocs",
                table: "workShopLocs",
                column: "WorkShopId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "A1", "1", "Admin", "ADMIN" },
                    { "T1", "2", "Technician", "TECHNICIAN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_workShopLocs_WorkshopId",
                table: "Equipment",
                column: "WorkshopId",
                principalTable: "workShopLocs",
                principalColumn: "WorkShopId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
