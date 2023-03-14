using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class troubleshoot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Troubleshooters_DeviceTypes_DeviceTypeName",
                table: "Troubleshooters");

            migrationBuilder.DropIndex(
                name: "IX_Troubleshooters_DeviceTypeName",
                table: "Troubleshooters");

            migrationBuilder.DropColumn(
                name: "DeviceTypeName",
                table: "Troubleshooters");

            migrationBuilder.AlterColumn<string>(
                name: "Recommendation",
                table: "Troubleshooters",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ConfigurationKey",
                table: "Troubleshooters",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "Troubleshooters",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "Troubleshooters");

            migrationBuilder.AlterColumn<string>(
                name: "Recommendation",
                table: "Troubleshooters",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfigurationKey",
                table: "Troubleshooters",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceTypeName",
                table: "Troubleshooters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Troubleshooters_DeviceTypeName",
                table: "Troubleshooters",
                column: "DeviceTypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Troubleshooters_DeviceTypes_DeviceTypeName",
                table: "Troubleshooters",
                column: "DeviceTypeName",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
