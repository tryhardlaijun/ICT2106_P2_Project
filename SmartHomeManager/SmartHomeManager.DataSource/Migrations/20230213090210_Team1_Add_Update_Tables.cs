using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Team1AddUpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigurationValue",
                table: "RuleHistories");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "RuleHistories",
                newName: "RuleStartTime");

            migrationBuilder.RenameColumn(
                name: "RuleNum",
                table: "RuleHistories",
                newName: "RuleIndex");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "RuleHistories",
                newName: "RuleEndTime");

            migrationBuilder.RenameColumn(
                name: "ActionTrigger",
                table: "RuleHistories",
                newName: "RuleActionTrigger");

            migrationBuilder.AddColumn<string>(
                name: "DeviceConfiguration",
                table: "RuleHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RuleName",
                table: "RuleHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceAdjustedConfiguration",
                table: "Histories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "APIDatas",
                columns: table => new
                {
                    APIDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Specification = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIDatas", x => x.APIDataId);
                });

            migrationBuilder.CreateTable(
                name: "APIKeys",
                columns: table => new
                {
                    APIKeyType = table.Column<Guid>(type: "TEXT", nullable: false),
                    APILabelText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIKeys", x => x.APIKeyType);
                });

            migrationBuilder.CreateTable(
                name: "HomeSecurities",
                columns: table => new
                {
                    HomeSecurityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SecurityModeState = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSecurities", x => x.HomeSecurityId);
                    table.ForeignKey(
                        name: "FK_HomeSecurities_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeSecurityDeviceDefinitions",
                columns: table => new
                {
                    DeviceGroup = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationOffValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationOnValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSecurityDeviceDefinitions", x => x.DeviceGroup);
                });

            migrationBuilder.CreateTable(
                name: "APIValues",
                columns: table => new
                {
                    APIKeyType = table.Column<Guid>(type: "TEXT", nullable: false),
                    APIValues = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIValues", x => x.APIKeyType);
                    table.ForeignKey(
                        name: "FK_APIValues_APIKeys_APIKeyType",
                        column: x => x.APIKeyType,
                        principalTable: "APIKeys",
                        principalColumn: "APIKeyType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeSecuritySettings",
                columns: table => new
                {
                    HomeSecuritySettingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceGroup = table.Column<string>(type: "TEXT", nullable: false),
                    HomeSecurityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeSecuritySettings", x => x.HomeSecuritySettingId);
                    table.ForeignKey(
                        name: "FK_HomeSecuritySettings_HomeSecurities_HomeSecurityId",
                        column: x => x.HomeSecurityId,
                        principalTable: "HomeSecurities",
                        principalColumn: "HomeSecurityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeSecuritySettings_HomeSecurityDeviceDefinitions_DeviceGroup",
                        column: x => x.DeviceGroup,
                        principalTable: "HomeSecurityDeviceDefinitions",
                        principalColumn: "DeviceGroup",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeSecurities_AccountId",
                table: "HomeSecurities",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSecuritySettings_DeviceGroup",
                table: "HomeSecuritySettings",
                column: "DeviceGroup");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSecuritySettings_HomeSecurityId",
                table: "HomeSecuritySettings",
                column: "HomeSecurityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIDatas");

            migrationBuilder.DropTable(
                name: "APIValues");

            migrationBuilder.DropTable(
                name: "HomeSecuritySettings");

            migrationBuilder.DropTable(
                name: "APIKeys");

            migrationBuilder.DropTable(
                name: "HomeSecurities");

            migrationBuilder.DropTable(
                name: "HomeSecurityDeviceDefinitions");

            migrationBuilder.DropColumn(
                name: "DeviceConfiguration",
                table: "RuleHistories");

            migrationBuilder.DropColumn(
                name: "RuleName",
                table: "RuleHistories");

            migrationBuilder.DropColumn(
                name: "DeviceAdjustedConfiguration",
                table: "Histories");

            migrationBuilder.RenameColumn(
                name: "RuleStartTime",
                table: "RuleHistories",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "RuleIndex",
                table: "RuleHistories",
                newName: "RuleNum");

            migrationBuilder.RenameColumn(
                name: "RuleEndTime",
                table: "RuleHistories",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "RuleActionTrigger",
                table: "RuleHistories",
                newName: "ActionTrigger");

            migrationBuilder.AddColumn<int>(
                name: "ConfigurationValue",
                table: "RuleHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
