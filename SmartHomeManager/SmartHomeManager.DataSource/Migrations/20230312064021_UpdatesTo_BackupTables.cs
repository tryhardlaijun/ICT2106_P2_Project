using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesToBackupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BackupScenarios",
                table: "BackupScenarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BackupRules",
                table: "BackupRules");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                table: "BackupScenarios");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                table: "BackupRules");

            migrationBuilder.AddColumn<Guid>(
                name: "BackupId",
                table: "BackupScenarios",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BackupId",
                table: "BackupRules",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BackupScenarios",
                table: "BackupScenarios",
                columns: new[] { "BackupId", "ScenarioId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BackupRules",
                table: "BackupRules",
                columns: new[] { "BackupId", "ScenarioId", "RuleId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BackupScenarios",
                table: "BackupScenarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BackupRules",
                table: "BackupRules");

            migrationBuilder.DropColumn(
                name: "BackupId",
                table: "BackupScenarios");

            migrationBuilder.DropColumn(
                name: "BackupId",
                table: "BackupRules");

            migrationBuilder.AddColumn<float>(
                name: "VersionNumber",
                table: "BackupScenarios",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "VersionNumber",
                table: "BackupRules",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BackupScenarios",
                table: "BackupScenarios",
                columns: new[] { "ScenarioId", "VersionNumber" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BackupRules",
                table: "BackupRules",
                columns: new[] { "RuleId", "VersionNumber" });
        }
    }
}
