using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Timezone = table.Column<int>(type: "INTEGER", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConfigurationLookUps",
                columns: table => new
                {
                    ConfigurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceBrand = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationValue = table.Column<string>(type: "TEXT", nullable: false),
                    ValueMeaning = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigurationLookUps", x => new { x.ConfigurationKey, x.DeviceBrand, x.DeviceModel });
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    DeviceTypeName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.DeviceTypeName);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NotificationMessage = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profile_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    XCoordinate = table.Column<int>(type: "INTEGER", nullable: false),
                    YCoordinate = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    ScenarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScenarioName = table.Column<string>(type: "TEXT", nullable: false),
                    RuleList = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.ScenarioId);
                    table.ForeignKey(
                        name: "FK_Scenarios_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceBrand = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeName",
                        column: x => x.DeviceTypeName,
                        principalTable: "DeviceTypes",
                        principalColumn: "DeviceTypeName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConfigurations",
                columns: table => new
                {
                    ConfigurationKey = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceBrand = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigurations", x => new { x.ConfigurationKey, x.DeviceId });
                    table.ForeignKey(
                        name: "FK_DeviceConfigurations_DeviceConfigurationLookUps_ConfigurationKey_DeviceBrand_DeviceModel",
                        columns: x => new { x.ConfigurationKey, x.DeviceBrand, x.DeviceModel },
                        principalTable: "DeviceConfigurationLookUps",
                        principalColumns: new[] { "ConfigurationKey", "DeviceBrand", "DeviceModel" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceConfigurations_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceLogs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateLogged = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeviceEnergyUsage = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceActivity = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceState = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_DeviceLogs_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceProfile",
                columns: table => new
                {
                    DevicesDeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfilesProfileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceProfile", x => new { x.DevicesDeviceId, x.ProfilesProfileId });
                    table.ForeignKey(
                        name: "FK_DeviceProfile_Devices_DevicesDeviceId",
                        column: x => x.DevicesDeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceProfile_Profile_ProfilesProfileId",
                        column: x => x.ProfilesProfileId,
                        principalTable: "Profile",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScenarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConfigurationValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionTrigger = table.Column<string>(type: "TEXT", nullable: true),
                    ScheduleName = table.Column<string>(type: "TEXT", nullable: true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rules_Scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "Scenarios",
                        principalColumn: "ScenarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Troubleshooters",
                columns: table => new
                {
                    TroubleshooterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Recommendation = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troubleshooters", x => x.TroubleshooterId);
                    table.ForeignKey(
                        name: "FK_Troubleshooters_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigurations_ConfigurationKey_DeviceBrand_DeviceModel",
                table: "DeviceConfigurations",
                columns: new[] { "ConfigurationKey", "DeviceBrand", "DeviceModel" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigurations_DeviceId",
                table: "DeviceConfigurations",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceLogs_DeviceId",
                table: "DeviceLogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceProfile_ProfilesProfileId",
                table: "DeviceProfile",
                column: "ProfilesProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AccountId",
                table: "Devices",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeName",
                table: "Devices",
                column: "DeviceTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_RoomId",
                table: "Devices",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AccountId",
                table: "Notifications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_AccountId",
                table: "Profile",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AccountId",
                table: "Rooms",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_DeviceId",
                table: "Rules",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_ScenarioId",
                table: "Rules",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Scenarios_ProfileId",
                table: "Scenarios",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Troubleshooters_DeviceId",
                table: "Troubleshooters",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceConfigurations");

            migrationBuilder.DropTable(
                name: "DeviceLogs");

            migrationBuilder.DropTable(
                name: "DeviceProfile");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Troubleshooters");

            migrationBuilder.DropTable(
                name: "DeviceConfigurationLookUps");

            migrationBuilder.DropTable(
                name: "Scenarios");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
