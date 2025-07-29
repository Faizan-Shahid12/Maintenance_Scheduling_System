using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maintenance_Scheduling_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameandAddedNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTask_MaintenanceSchedules_ScheduleId",
                table: "ScheduleTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLogs_Tasks_TaskId",
                table: "TaskLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_TechnicianId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Equipment_EquipmentId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_MaintenanceHistories_HistoryId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleTask",
                table: "ScheduleTask");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "MainTask");

            migrationBuilder.RenameTable(
                name: "ScheduleTask",
                newName: "ScheduleTasks");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TechnicianId",
                table: "MainTask",
                newName: "IX_MainTask_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_HistoryId",
                table: "MainTask",
                newName: "IX_MainTask_HistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_EquipmentId",
                table: "MainTask",
                newName: "IX_MainTask_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleTask_ScheduleId",
                table: "ScheduleTasks",
                newName: "IX_ScheduleTasks_ScheduleId");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ScheduleTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ScheduleTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EquipmentName",
                table: "ScheduleTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ScheduleTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "ScheduleTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ScheduleTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainTask",
                table: "MainTask",
                column: "TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleTasks",
                table: "ScheduleTasks",
                column: "ScheduleTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_MainTask_AspNetUsers_TechnicianId",
                table: "MainTask",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MainTask_Equipment_EquipmentId",
                table: "MainTask",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MainTask_MaintenanceHistories_HistoryId",
                table: "MainTask",
                column: "HistoryId",
                principalTable: "MaintenanceHistories",
                principalColumn: "HistoryId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTasks_MaintenanceSchedules_ScheduleId",
                table: "ScheduleTasks",
                column: "ScheduleId",
                principalTable: "MaintenanceSchedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLogs_MainTask_TaskId",
                table: "TaskLogs",
                column: "TaskId",
                principalTable: "MainTask",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainTask_AspNetUsers_TechnicianId",
                table: "MainTask");

            migrationBuilder.DropForeignKey(
                name: "FK_MainTask_Equipment_EquipmentId",
                table: "MainTask");

            migrationBuilder.DropForeignKey(
                name: "FK_MainTask_MaintenanceHistories_HistoryId",
                table: "MainTask");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleTasks_MaintenanceSchedules_ScheduleId",
                table: "ScheduleTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLogs_MainTask_TaskId",
                table: "TaskLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleTasks",
                table: "ScheduleTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainTask",
                table: "MainTask");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ScheduleTasks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ScheduleTasks");

            migrationBuilder.DropColumn(
                name: "EquipmentName",
                table: "ScheduleTasks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ScheduleTasks");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "ScheduleTasks");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ScheduleTasks");

            migrationBuilder.RenameTable(
                name: "ScheduleTasks",
                newName: "ScheduleTask");

            migrationBuilder.RenameTable(
                name: "MainTask",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleTasks_ScheduleId",
                table: "ScheduleTask",
                newName: "IX_ScheduleTask_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_MainTask_TechnicianId",
                table: "Tasks",
                newName: "IX_Tasks_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_MainTask_HistoryId",
                table: "Tasks",
                newName: "IX_Tasks_HistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MainTask_EquipmentId",
                table: "Tasks",
                newName: "IX_Tasks_EquipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleTask",
                table: "ScheduleTask",
                column: "ScheduleTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleTask_MaintenanceSchedules_ScheduleId",
                table: "ScheduleTask",
                column: "ScheduleId",
                principalTable: "MaintenanceSchedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLogs_Tasks_TaskId",
                table: "TaskLogs",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_TechnicianId",
                table: "Tasks",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Equipment_EquipmentId",
                table: "Tasks",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_MaintenanceHistories_HistoryId",
                table: "Tasks",
                column: "HistoryId",
                principalTable: "MaintenanceHistories",
                principalColumn: "HistoryId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
