using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class backuptables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackupRule",
                columns: table => new
                {
                    rulesID = table.Column<Guid>(type: "TEXT", nullable: false),
                    scenarioID = table.Column<Guid>(type: "TEXT", nullable: false),
                    scheduleName = table.Column<string>(type: "TEXT", nullable: false),
                    startTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    endTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    actionTrigger = table.Column<string>(type: "TEXT", nullable: false),
                    configurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    configurationValue = table.Column<int>(type: "INTEGER", nullable: false),
                    apiKey = table.Column<string>(type: "TEXT", nullable: false),
                    apiValue = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackupRule", x => x.rulesID);
                });

            migrationBuilder.CreateTable(
                name: "BackupScenario",
                columns: table => new
                {
                    scenarioID = table.Column<Guid>(type: "TEXT", nullable: false),
                    scheduleName = table.Column<string>(type: "TEXT", nullable: false),
                    profileID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackupScenario", x => x.scenarioID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackupRule");

            migrationBuilder.DropTable(
                name: "BackupScenario");
        }
    }
}
