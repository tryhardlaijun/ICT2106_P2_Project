using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class ChangedScheduleNameToRuleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleName",
                table: "Rules");

            migrationBuilder.AddColumn<string>(
                name: "RuleName",
                table: "Rules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuleName",
                table: "Rules");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleName",
                table: "Rules",
                type: "TEXT",
                nullable: true);
        }
    }
}
