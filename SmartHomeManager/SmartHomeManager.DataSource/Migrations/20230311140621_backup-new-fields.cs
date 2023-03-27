using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class backupnewfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "BackupScenario",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "versionNumber",
                table: "BackupScenario",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "versionNumber",
                table: "BackupRule",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "BackupScenario");

            migrationBuilder.DropColumn(
                name: "versionNumber",
                table: "BackupScenario");

            migrationBuilder.DropColumn(
                name: "versionNumber",
                table: "BackupRule");
        }
    }
}
